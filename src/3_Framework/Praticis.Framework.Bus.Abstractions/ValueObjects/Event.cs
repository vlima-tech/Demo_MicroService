
using System;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.Events;

namespace Praticis.Framework.Bus.Abstractions.ValueObjects
{
    /// <summary>
    /// Represents an event of application.
    /// </summary>
    public abstract class Event : IEvent
    {
        #region Properties

        /// <summary>
        /// The Event Id.
        /// </summary>
        public Guid EventId { get; protected set; }

        /// <summary>
        /// The event name.
        /// </summary>
        public string EventName { get; }

        /// <summary>
        /// The time that the event occurred.
        /// </summary>
        public DateTime Time { get; protected set; }

        /// <summary>
        /// The event type represented by command.
        /// </summary>
        public EventType EventType { get; protected set; }

        /// <summary>
        /// The execution mode of event.
        /// </summary>
        public ExecutionMode ExecutionMode { get; protected set; }

        /// <summary>
        /// The store mode of event.
        /// </summary>
        public EventStoreMode StoreMode { get; protected set; }

        /// <summary>
        /// The assembly context of event.
        /// </summary>
        public Type ResourceType { get; set; }

        /// <summary>
        /// The work type of event.
        /// </summary>
        public WorkType WorkType { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an event of application.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="executionMode">The execution mode of event.</param>
        /// <param name="storeMode">The mode that event informations will be stored.</param>
        protected Event(EventType eventType, ExecutionMode executionMode, EventStoreMode storeMode)
        {
            this.EventId = Guid.NewGuid();
            this.EventName = this.GetType().Name;
            this.Time = DateTime.Now;
            this.ResourceType = this.GetType();
            this.EventType = eventType;
            this.ExecutionMode = executionMode;
            this.StoreMode = storeMode;
            this.WorkType = WorkType.Event;
        }

        /// <summary>
        /// Initializes an event of application.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="eventType">The type of event.</param>
        /// <param name="executionMode">The execution mode of event.</param>
        /// <param name="storeMode">The mode that event informations will be stored.</param>
        protected Event(Guid eventId, EventType eventType, ExecutionMode executionMode, EventStoreMode storeMode)
            : this(eventType, executionMode, storeMode)
        {
            this.EventId = eventId;
        }

        #endregion

        public void ChangeExecutionMode(ExecutionMode executionMode)
            => this.ExecutionMode = executionMode;

        public Guid ObtainsRequestId() => this.EventId;

        public void ChangeRequestId(Guid id) => this.EventId = id;

        public string ObtainsWorkName() => this.EventName;
    }
}