
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Commands;
using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;
using Praticis.Framework.Worker.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions.Repositories;
using Praticis.Framework.Worker.Abstractions.ValueObjects;

namespace Praticis.Framework.Worker.Core
{
    public class QueueHostedService
    {
        #region Properties

        private readonly IServiceBus _serviceBus;
        public QueueType QueueId { get; private set; }
        private Queue<Work> _enqueuedWorks { get; set; }
        private List<Work> _worksInExecution { get; set; }
        private List<Task> _tasksInExecution { get; set; }
        private CancellationToken _cancellationToken { get; set; }
        private int _worksInEnqueProcess { get; set; }
        private DateTime? _lastEnqueueProcess { get; set; }
        public DateTime LastReload { get; private set; }
        public QueueSetting Settings { get; private set; }
        public bool Initialized { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsEmpty => this._enqueuedWorks.Count == 0;
        public bool InSafeMode { get; private set; }
        public int EmptyCapacity => this.Settings.MaxLength - this._enqueuedWorks.Count;
        private Task reloadTask { get; set; }
        private IServiceProvider _provider { get; set; }
        #endregion

        #region Constructor

        public QueueHostedService(IServiceBus serviceBus, IServiceProvider provider, 
            QueueSetting queueSetting, bool startQueue = false)
        {
            this._serviceBus = serviceBus;
            this.QueueId = queueSetting.QueueId;
            this.Settings = queueSetting;
            this._provider = provider;
            this._worksInEnqueProcess = 0;
            this.Initialized = false;
            this.IsRunning = false;
            this.InSafeMode = true;

            if (startQueue)
                Task.Run(this.InitializeAsync);
        }

        #endregion

        public Task InitializeAsync()
        {
            this._enqueuedWorks = new Queue<Work>();
            this._tasksInExecution = new List<Task>();
            this._worksInExecution = new List<Work>();
            this._cancellationToken = new CancellationToken(false);
            this.Initialized = true;

            if (!IsRunning)
                Task.Run(RunAsync);

            return Task.CompletedTask;
        }

        private async Task RunAsync()
        {
            this.IsRunning = true;

            while (!this._cancellationToken.IsCancellationRequested)
            {
                if (this.CanRequestReload())
                    this.reloadTask = Task.Run(() => this.ReloadAsync());

                if (this.CanExecuteNewTask())
                {
                    var work = this._enqueuedWorks.Dequeue();
                    var task = this.StartTask(work);
                    work.DefineTaskId(task.Id);

                    this._worksInExecution.Add(work);
                    this._tasksInExecution.Add(task);

                    // Não coloque 'await' pois senão irá travar a fila
                    task.ContinueWith(DequeueExecutedTask);
                }
                else
                    Thread.Sleep(this.Settings.SleepInterval);
            }

            this.IsRunning = false;
        }

        private Task StartTask(Work work)
        {
            Task task;
            IWorkRepository workRepository;

            work.Request.ChangeExecutionMode(ExecutionMode.WaitToClose);

            switch (work.Request.WorkType)
            {
                case WorkType.Command:
                    task = this._serviceBus.SendCommand((ICommand)work.Request);
                    break;
                case WorkType.Event:
                    task = this._serviceBus.PublishEvent((IEvent)work.Request);
                    break;
                default:
                    task = Task.CompletedTask;
                    break;
            }

            work.DefineTaskId(task.Id);

            work.StartProcessWhen(DateTime.Now);

            workRepository = this._provider.CreateScope().ServiceProvider.GetService<IWorkRepository>();

            workRepository.SaveAsync(work).GetAwaiter().GetResult();
            workRepository.Commit();

            workRepository.Dispose();
            workRepository = null;

            this._serviceBus.PublishEvent(new WorkStartedEvent(work))
                .GetAwaiter()
                .GetResult();

            /* 
                                    #### ATENÇÃO ###
             
                Este método NÃO É ASSINCRONO!! O motivo pelo qual retorna-se uma Task
                é devido ao objeto Task ser a tarefa em execução do Work (Command ou Event).

                Se colocar await, a task recebida como retorno NÃO SERÁ a mesma que contém
                a execução do Command ou Event e, por tanto, não será possível realizar o
                vínculo da Task com o respectivo work.
            */
            return task;
        }

        private async Task DequeueExecutedTask(Task task)
        {
            var work = this._worksInExecution.Where(w => w.TaskId == task.Id)
                .FirstOrDefault();

            this._worksInExecution.Remove(work);
            this._tasksInExecution.Remove(task);

            work.FinishProcessWhen(DateTime.Now);
            work.ChangeStatusTo(WorkStatus.Processed);

            var workRepository = this._provider.CreateScope().ServiceProvider.GetService<IWorkRepository>();

            await workRepository.SaveAsync(work);
            await workRepository.CommitAsync();

            workRepository.Dispose();
            workRepository = null;

            await _serviceBus.PublishEvent(new WorkFinishedEvent(work, task.Id, DateTime.Now));
        }


        private void Enqueue(IEnumerable<Work> works)
        {
            IWorkRepository workRepository;

            foreach (var item in works)
                item.EnqueuedWhen(DateTime.Now);

            workRepository = this._provider.CreateScope().ServiceProvider.GetService<IWorkRepository>();

            workRepository.SaveRangeAsync(works);
            workRepository.Commit();

            workRepository.Dispose();
            workRepository = null;

            foreach (var item in works)
                this._enqueuedWorks.Enqueue(item);
        }

        /// <summary>
        /// Obtains new works from database to enqueue in memory and process.
        /// The reload count is based in queue empty capacity.
        /// </summary>
        /// <param name="safeMode"></param>
        /// <returns></returns>
        private Task ReloadAsync(bool safeMode = false)
        {
            Expression<Func<Work, bool>> loadSpecification, safeModeLoadSpecification;
            List<Work> works = new List<Work>();
            IWorkReadRepository workReadRepository;

            this.LastReload = DateTime.Now;
            workReadRepository = this._provider.CreateScope().ServiceProvider.GetService<IWorkReadRepository>();

            safeModeLoadSpecification = WorkSpec.LoadWorks(this.QueueId, WorkStatus.Processing | WorkStatus.Enqueued);
            loadSpecification = WorkSpec.LoadWorks(this.QueueId, WorkStatus.Created);

            // recovery possible works that has executing but system was stopped and having aborted executions.
            if (safeMode || this.InSafeMode)
            {
                works = workReadRepository.Query(safeModeLoadSpecification, 1, this.EmptyCapacity, true)
                    .OrderBy(w => w.EnqueueDate)
                    .ToList();
            }

            // reload new works after all aborted execution has enqueued.
            if(this.EmptyCapacity - works.Count > 0)
            {
                this.InSafeMode = false;
                int remainingCapacity = this.EmptyCapacity - works.Count;

                works.AddRange(
                    workReadRepository.Query(loadSpecification, 1, remainingCapacity, true)
                        .OrderBy(w => w.CreationDate)
                        .ToList()
                );
            }
            else
            { }

            if (works.Count > 0)
                this.Enqueue(works);

            this._lastEnqueueProcess = DateTime.Now;

            workReadRepository.Dispose();
            workReadRepository = null;

            return Task.CompletedTask;
        }

        private bool CanExecuteNewTask()
        {
            if (this._enqueuedWorks.Count > 0 && this._enqueuedWorks.Count == this._worksInEnqueProcess)
                return false;

            return this._enqueuedWorks.Count > 0 && this._tasksInExecution.CanExecuteNewTask(Settings.MaxWIP);
        }

        private bool CanRequestReload()
        {
            if (this.reloadTask is null)
                return true;

            if (!this.reloadTask.IsCompleted)
                return false;

            var lastReload = DateTime.Now - this.LastReload;

            if (lastReload < this.Settings.ReloadInterval)
                return false;

            if (this.IsEmpty || this._enqueuedWorks.Count <= this.Settings.ReloadLevel)
                return true;

            return false;
        }

        public Task StopAsync()
        {
            this._cancellationToken = new CancellationToken(true);

            while (this.IsRunning)
                Thread.Sleep(1000);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}