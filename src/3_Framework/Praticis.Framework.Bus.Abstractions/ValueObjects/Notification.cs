
using System;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Abstractions.ValueObjects
{
    /// <summary>
    /// Represents a Notification of Application.
    /// </summary>
    public class Notification : Event
    {
        /// <summary>
        /// The code of Notification.
        /// </summary>
        public string Code { get; protected set; }

        /// <summary>
        /// The message of notification.
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// The type of notification.
        /// </summary>
        public NotificationType NotificationType { get; private set; }

        #region Constructors

        /// <summary>
        /// Initializes a system notification message.
        /// </summary>
        /// <param name="eventId">The notification id.</param>
        /// <param name="code">The notification code.</param>
        /// <param name="value">The notification message.</param>
        public Notification(Guid eventId, string code, string value)
            : base(eventId, EventType.Domain_Notification, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Code = code;
            this.Value = value;
            this.NotificationType = NotificationType.Notification;
        }

        /// <summary>
        /// Initializes a system notification message.
        /// </summary>
        /// <param name="eventId">The notification id.</param>
        /// <param name="code">The notification code.</param>
        /// <param name="value">The notification value.</param>
        /// <param name="notificationType">The notification type.</param>
        protected Notification(Guid eventId, string code, string value, NotificationType notificationType)
            : base(eventId, EventType.Domain_Notification, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Code = code;
            this.Value = value;
            this.NotificationType = notificationType;
        }

        /// <summary>
        /// Initializes a system notification message.
        /// </summary>
        /// <param name="code">The notification code.</param>
        /// <param name="value">The notification message.</param>
        public Notification(string code, string value)
            : base(EventType.Domain_Notification, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Code = code;
            this.Value = value;
            this.NotificationType = NotificationType.Notification;
        }

        /// <summary>
        /// Initializes a system notification message.
        /// </summary>
        /// <param name="code">The notification code.</param>
        /// <param name="value">The notification message.</param>
        /// <param name="notificationType">The notification type.</param>
        protected Notification(string code, string value, NotificationType notificationType)
            : base(EventType.Domain_Notification, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Code = code;
            this.Value = value;
            this.NotificationType = notificationType;
        }

        /// <summary>
        /// Initializes a system notification message.
        /// </summary>
        /// <param name="value">The notification message.</param>
        public Notification(string value)
            : base(EventType.Domain_Notification, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Value = value;
            this.NotificationType = NotificationType.Notification;
        }

        /// <summary>
        /// Initializes a system notification message.
        /// </summary>
        /// <param name="value">The notification message.</param>
        /// <param name="notificationType">The notification type.</param>
        protected Notification(string value, NotificationType notificationType)
            : base(EventType.Domain_Notification, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Value = value;
            this.NotificationType = notificationType;
        }

        #endregion

        public override string ToString() => this.Value;

        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}