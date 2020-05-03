
using System;

namespace Praticis.Framework.Bus.Abstractions.Enums
{
    /// <summary>
    /// Represents the system notification types.
    /// </summary>
    [Flags]
    public enum NotificationType
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
        /// Represents a pipeline execution notification.
        /// </summary>
        Pipeline = 5,
    }
}