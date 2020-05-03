
using System;

namespace Praticis.Framework.Bus.Abstractions.Enums
{
    /// <summary>
    /// Represents the system events.
    /// </summary>
    [Flags]
    public enum EventType
    {
        /// <summary>
        /// Represents a domain notification event.
        /// </summary>
        Domain_Notification = 1,

        /// <summary>
        /// Represents a non critical system error. But a message that need be show to User.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Represents an entry log of the system.
        /// </summary>
        Log = 3,

        /// <summary>
        /// Represents an error of System. Contains sensitive informations and this can't be show to user.
        /// </summary>
        System_Error = 4,

        /// <summary>
        /// Represents a pipline finished execution event.
        /// </summary>
        Pipeline_Finished = 5,

        /// <summary>
        /// A work was created and waiting to enqueued after.
        /// </summary>
        Work_Created = 6,

        /// <summary>
        /// A work is enqueued in memory waiting to start.
        /// </summary>
        Work_Enqueued = 7,

        /// <summary>
        /// A work started.
        /// </summary>
        Work_Started = 8,

        /// <summary>
        /// A work finished execution successfully event.
        /// </summary>
        Work_Finished = 9,

        /// <summary>
        /// A work failed execution.
        /// Is published when a work execution failed.
        /// </summary>
        Work_Failed = 10,

        /// <summary>
        /// Represents a new customer registration.
        /// </summary>
        Registered_Customer = 11,
    }
}