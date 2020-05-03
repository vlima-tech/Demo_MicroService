
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
        /// The id of root entity.
        /// </summary>
        public Guid? AggregateId { get; protected set; }

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
        public Command(ExecutionMode executionMode = ExecutionMode.WaitToClose)
        {
            this.CommandId = Guid.NewGuid();
            this.Time = DateTime.Now;
            this.ResourceType = this.GetType();
            this.CommandName = this.GetType().Name;
            this.ExecutionMode = executionMode;
            this.WorkType = WorkType.Command;
        }


        /// <summary>
        /// Initializes a command.
        /// </summary>
        /// <param name="commandName">The command name.</param>
        /// <param name="executionMode">
        /// 'Wait To Close' mode will be executed imemdiately. 
        /// 'Queue' mode will be queued to execute in background.
        /// </param>
        public Command(string commandName, ExecutionMode executionMode = ExecutionMode.WaitToClose)
            : this(executionMode)
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a command.
        /// </summary>
        /// <param name="aggregateId">The aggregate id root entity.</param>
        /// /// <param name="executionMode">
        /// 'Wait To Close' mode will be executed imemdiately. 
        /// 'Queue' mode will be queued to execute in background.
        /// </param>
        public Command(Guid aggregateId, ExecutionMode executionMode = ExecutionMode.WaitToClose)
            : this(executionMode)
        {
            this.AggregateId = aggregateId;
        }

        #endregion

        /// <summary>
        /// Change execution mode of command.
        /// </summary>
        /// <param name="executionMode"></param>
        public void ChangeExecutionMode(ExecutionMode executionMode) 
            => this.ExecutionMode = executionMode;

        public Guid GetRequestId() => this.CommandId;

        public void ChangeRequestId(Guid id) => this.CommandId = id;
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
        /// The id of root entity.
        /// </summary>
        public Guid? AggregateId { get; protected set; }

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

        /// <summary>
        /// Initializes a command.
        /// </summary>
        /// <param name="aggregateId">The aggregate id root entity.</param>
        public Command(Guid aggregateId)
            : this()
        {
            this.AggregateId = aggregateId;
        }

        #endregion

        public void DefineResponse(TResponse response) => this.Response = response;
    }
}