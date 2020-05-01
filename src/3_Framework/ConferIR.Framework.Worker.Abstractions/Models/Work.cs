
using System;
using System.Diagnostics.CodeAnalysis;

using FluentValidation.Results;
using Newtonsoft.Json;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Commands;
using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Layers.Domain.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Worker.Abstractions
{
    public class Work : IModel
    {
        #region Properties

        /// <summary>
        /// The work id.
        /// </summary>
        public Guid Id { get; private set; }

        public QueueType QueueId { get; private set; }

        public int TaskId { get; private set; }

        /// <summary>
        /// The work name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The creation date.
        /// </summary>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// The date of enqueue.
        /// </summary>
        public DateTime? EnqueueDate { get; private set; }

        /// <summary>
        /// The start date of processing.
        /// </summary>
        public DateTime? StartProcessDate { get; private set; }

        /// <summary>
        /// The finish date of end processing.
        /// </summary>
        public DateTime? FinishProcessDate { get; private set; }

        /// <summary>
        /// The status of work.
        /// </summary>
        public WorkStatus Status { get; private set; }

        /// <summary>
        /// The Command or Event serialized
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// The log execution of work.
        /// </summary>
        public string ExecutionLog { get; private set; }

        /// <summary>
        /// The work, a command or an event.
        /// </summary>
        private IWork _request;
        public IWork Request
        {
            get
            {
                if (this._request == null && this.Data != null)
                {
                    this._request = (IWork)JsonConvert.DeserializeObject(this.Data, this.RequestType);
                    this._request.ChangeRequestId(this.Id);
                }

                return this._request;
            }
            private set
            {
                this._request = value;

                if (value is null)
                    this.Data = string.Empty;
                else
                {
                    this.Data = JsonConvert.SerializeObject(value);
                    this.RequestType = value.GetType();
                }
            }
        }

        public Type RequestType { get; private set; }

        public bool IsValid => this.Validate();

        public ValidationResult Notifications { get; private set; }

        #endregion

        #region Constructors

        protected Work()
        { }

        public Work(IWork work)
        {
            this.Id = work.GetRequestId();
            this.Request = work;
            this.Status = WorkStatus.Created;
            this.Name = this.GetWorkName(work);
            this.CreationDate = DateTime.Now;
        }

        #endregion

        /// <summary>
        /// Define a enqueued date and change Status property to 'Enqueued'.
        /// </summary>
        /// <param name="enqueueDate"></param>
        public void EnqueuedWhen(DateTime enqueueDate)
        {
            if (enqueueDate != default && enqueueDate > this.CreationDate)
            {
                this.Status = WorkStatus.Enqueued;
                this.EnqueueDate = enqueueDate;
            }
        }

        /// <summary>
        /// Define a star process date and change Status property to 'Processing'.
        /// </summary>
        /// <param name="startDate"></param>
        public void StartProcessWhen(DateTime startDate)
        {
            if (startDate != default && (this.EnqueueDate.HasValue && startDate > this.EnqueueDate.Value))
            {
                this.Status = WorkStatus.Processing;
                this.StartProcessDate = startDate;
            }
        }

        /// <summary>
        /// Define a finish process date. Status property do not changed because
        /// can not identify final work status (finished, failed, canceled, etc).
        /// </summary>
        /// <param name="finishDate"></param>
        public void FinishProcessWhen(DateTime finishDate)
        {
            if (finishDate != default && (this.StartProcessDate.HasValue && finishDate > this.StartProcessDate.Value))
                this.FinishProcessDate = finishDate;
        }

        /// <summary>
        /// Define execution log of execution work.
        /// </summary>
        /// <param name="log"></param>
        public void DefineExecutionLog(string log)
        {
            if (string.IsNullOrEmpty(this.ExecutionLog))
                this.ExecutionLog = log;
        }

        /// <summary>
        /// Change work status.
        /// </summary>
        /// <param name="status"></param>
        public void ChangeStatusTo(WorkStatus status)
        {
            bool canChange = false;

            if (status > WorkStatus.Processed)
                canChange = true;
            else if ((status >= WorkStatus.Created && status <= WorkStatus.Processed) && status > this.Status)
                canChange = true;

            if (canChange)
                this.Status = status;
        }

        /// <summary>
        /// Obtains request name.
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        private string GetWorkName([NotNull] IWork work)
        {
            string workName;

            if (work is ICommand)
                workName = (work as ICommand).CommandName;
            else
                workName = (work as IEvent).EventName;

            return workName;
        }

        public T GetRequest<T>()
        {
            if (this.RequestType == typeof(T))
                return (T)this.Request;

            return default;
        }

        public bool Validate()
        {
            return true;
        }

        /// <summary>
        /// Copy this property data to another object.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="changeId"></param>
        public void CopyTo([NotNull] IModel model, bool changeId = false)
        {
            if (!(model is Work))
                return;

            var work = model as Work;

            if (changeId)
                work.Id = this.Id;

            work.CreationDate = this.CreationDate;
            work.Data = this.Data;
            work.EnqueueDate = this.EnqueueDate;
            work.DefineExecutionLog(this.ExecutionLog);
            work.FinishProcessDate = this.FinishProcessDate;
            work.Name = this.Name;
            work.Request = this.Request;
            work.RequestType = this.RequestType;
            work.StartProcessDate = this.StartProcessDate;
            work.TaskId = this.TaskId;
            work.QueueId = this.QueueId;
            work.ChangeStatusTo(this.Status);
        }

        /// <summary>
        /// Change the queue id.
        /// </summary>
        /// <param name="queueType"></param>
        public void DefineQueueId(QueueType queueType) => this.QueueId = queueType;

        /// <summary>
        /// Define task execution id of the work.
        /// </summary>
        /// <param name="taskId"></param>
        public void DefineTaskId(int taskId) => this.TaskId = taskId;

        public void Dispose()
        {

        }
    }
}