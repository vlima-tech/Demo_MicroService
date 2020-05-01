
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
        /// Represents a pipline finished execution event.
        /// </summary>
        Pipeline_Finished = 2,

        /// <summary>
        /// A work was created and waiting to enqueued after.
        /// </summary>
        Work_Created = 3,

        /// <summary>
        /// A work is enqueued in memory waiting to start.
        /// </summary>
        Work_Enqueued = 4,

        /// <summary>
        /// A work started.
        /// </summary>
        Work_Started = 4,

        /// <summary>
        /// A work finished execution successfully event.
        /// </summary>
        Work_Finished = 5,

        /// <summary>
        /// A work failed execution.
        /// Is published when a work execution failed.
        /// </summary>
        Work_Failed = 6,

        /// <summary>
        /// An invalid ecac login.
        /// </summary>
        Ecac_Invalid_Login = 7,

        /// <summary>
        /// A valid ecac login.
        /// </summary>
        Ecac_Valid_Login = 8
    }
}