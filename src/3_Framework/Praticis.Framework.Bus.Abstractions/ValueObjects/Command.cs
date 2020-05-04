
using System;

using Praticis.Framework.Bus.Abstractions.Commands;
using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Abstractions.ValueObjects
{
    /// <summary>
    /// Represents a Order Execution that can be executed anywhere.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class Command : ICommand
    {
        #region Properties

        /// <summary>
        /// The command id.
        /// </summary>
        public Guid CommandId { get; protected set; }

        /// <summary>
        /// The command name.
        /// </summary>
        public string CommandName { get; protected set; }

        /// <summary>
        /// The creation command time.
        /// </summary>
        public DateTime Time { get; protected set; }

        /// <summary>
        /// The work type of command.
        /// </summary>
        public WorkType WorkType { get; }

        /// <summary>
        /// 'Wait To Close' mode will be executed imemdiately.
        /// 'Queue' mode will be queued to execute in background.
        /// </summary>
        public ExecutionMode ExecutionMode { get; protected set; }

        /// <summary>
        /// The event type represented by command.
        /// </summary>
        public EventType EventType { get; protected set; }

        /// <summary>
        /// The assembly context of command.
        /// </summary>
        public Type ResourceType { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a command.
        /// </summary>
        /// <param name="executionMode">
        /// 'Wait To Close' mode will be executed imemdiately. 
        /// 'Queue' mode will be queued to execute in background.
        /// </param>
        public Command(ExecutionMode executionMode = ExecutionMode.WaitToClose, EventType eventType = EventType.Default)
        {
            this.CommandId = Guid.NewGuid();
            this.Time = DateTime.Now;
            this.ResourceType = this.GetType();
            this.CommandName = this.GetType().Name;
            this.ExecutionMode = executionMode;
            this.EventType = eventType;
            this.WorkType = WorkType.Command;
        }

        #endregion

        /// <summary>
        /// Change execution mode of command.
        /// </summary>
        /// <param name="executionMode"></param>
        public void ChangeExecutionMode(ExecutionMode executionMode) 
            => this.ExecutionMode = executionMode;

        public Guid ObtainsRequestId() => this.CommandId;

        public void ChangeRequestId(Guid id) => this.CommandId = id;

        public string ObtainsWorkName() => this.CommandName;
    }

    /// <summary>
    /// Represents a Order Execution that can be shoot anywhere.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class Command<TResponse> : ICommand<TResponse>
    {
        /// <summary>
        /// The command id.
        /// </summary>
        public Guid CommandId { get; protected set; }

        /// <summary>
        /// The command name.
        /// </summary>
        public string CommandName { get; protected set; }

        /// <summary>
        /// The creation command time.
        /// </summary>
        public DateTime Time { get; protected set; }

        /// <summary>
        /// The assembly context of command.
        /// </summary>
        public Type ResourceType { get; protected set; }

        /// <summary>
        /// The output of command.
        /// </summary>
        public TResponse Response { get; protected set; }

        #region Constructors

        /// <summary>
        /// Initializes a command.
        /// </summary>
        public Command()
        {
            this.CommandId = Guid.NewGuid();
            this.Time = DateTime.Now;
            this.ResourceType = this.GetType();
            this.CommandName = this.GetType().Name;
        }

        #endregion

        public void DefineResponse(TResponse response) => this.Response = response;
    }
}