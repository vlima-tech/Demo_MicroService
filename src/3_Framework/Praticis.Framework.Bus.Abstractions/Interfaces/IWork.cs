
using System;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Abstractions
{
    /// <summary>
    /// Represents a request that can be enqueued, can be a Command or Event.
    /// Commands that return anything - Command<TResponse> - are not supported and can not be enqueued.
    /// </summary>
    public interface IWork
    {
        /// <summary>
        /// The request id can be CommandId, EventId, etc
        /// </summary>
        /// <returns></returns>
        Guid GetRequestId();

        /// <summary>
        /// Change the request id.
        /// </summary>
        /// <param name="id">The new id.</param>
        void ChangeRequestId(Guid id);

        /// <summary>
        /// The execution mode of work.
        /// </summary>
        ExecutionMode ExecutionMode { get; }
        
        /// <summary>
        /// The type of work.
        /// </summary>
        WorkType WorkType { get; }
        
        /// <summary>
        /// Change work execution mode.
        /// </summary>
        /// <param name="executionMode"></param>
        void ChangeExecutionMode(ExecutionMode executionMode);
    }
}