
using System;

namespace Praticis.Framework.Bus.Abstractions.Enums
{
    /// <summary>
    /// Represents the execution mode of a Command or Event.
    /// </summary>
    [Flags]
    public enum ExecutionMode
    {
        /// <summary>
        /// The Execution will be interrupted to Event Execution.
        /// </summary>
        WaitToClose = 1,

        /// <summary>
        /// The Event will be enqueued and executed when old works was executed.
        /// </summary>
        Enqueue = 2,
    }
}