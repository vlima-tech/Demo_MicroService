
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Commands;
using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;
using Praticis.Framework.Worker.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions.Repositories;
using Praticis.Framework.Worker.Abstractions.Settings;

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
        private Task _reloadTask { get; set; }
        private CancellationToken _cancellationToken { get; set; }
        public DateTime LastReload { get; private set; }
        public QueueOption Settings { get; private set; }
        private IQueueReadRepository _queueRepository { get; set; }
        public bool Initialized { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsEmpty => this._enqueuedWorks.Count == 0;
        public bool InSafeMode { get; private set; }
        public int EmptyCapacity => this.Settings.MaxLength - this._enqueuedWorks.Count;
        
        #endregion

        #region Constructor

        public QueueHostedService(IServiceBus serviceBus, IQueueReadRepository queueRepository,
            QueueOption queueSetting, bool startQueue = false)
        {
            this._serviceBus = serviceBus;
            this.QueueId = queueSetting.QueueId;
            this.Settings = queueSetting;
            this.Initialized = false;
            this.IsRunning = false;
            this.InSafeMode = true;
            this._queueRepository = queueRepository;

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
                    this._reloadTask = Task.Run(() => this.ReloadAsync());

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
                    return Task.CompletedTask;
            }

            this._serviceBus.PublishEvent(new StartedWorkEvent(work, task.Id, DateTime.Now))
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

            await _serviceBus.PublishEvent(new FinishedWorkEvent(work, task.Id, DateTime.Now));
        }

        private async Task EnqueueAsync(IEnumerable<Work> works)
        {
            List<Task> tasks = new List<Task>();
            foreach (var work in works)
            {
                this._enqueuedWorks.Enqueue(work);

                tasks.Add(_serviceBus.PublishEvent(new EnqueuedWorkEvent(work, DateTime.Now)));
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Obtains new works from database to enqueue in memory and process.
        /// The reload count is based in queue empty capacity.
        /// </summary>
        /// <param name="safeMode"></param>
        /// <returns></returns>
        private async Task ReloadAsync(bool safeMode = false)
        {
            this.LastReload = DateTime.Now;

            var works = await this._queueRepository.DequeueWorksAsync(this.EmptyCapacity, this._cancellationToken);

            if (works.Count() > 0)
                await this.EnqueueAsync(works);
        }

        private bool CanExecuteNewTask()
            => this._enqueuedWorks.Count > 0 && this._tasksInExecution.CanExecuteNewTask(Settings.MaxWIP);

        private bool CanRequestReload()
        {
            if (this._reloadTask is null)
                return true;

            if (!this._reloadTask.IsCompleted)
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
            this._cancellationToken = new CancellationToken(true);
        }
    }
}