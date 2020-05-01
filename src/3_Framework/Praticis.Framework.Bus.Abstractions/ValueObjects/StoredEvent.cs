
using System;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Abstractions.ValueObjects
{
    /// <summary>
    /// Represents the event serialized informations
    /// </summary>
    public class StoredEvent : IDisposable
    {
        /// <summary>
        /// The stored event id. It is not event id.
        /// </summary>
        public Guid StoredEventId { get; private set; }

        /// <summary>
        /// The type of event.
        /// </summary>
        public EventType EventType { get; private set; }

        /// <summary>
        /// The time that the event occurred.
        /// </summary>
        public DateTime Time { get; private set; }

        /// <summary>
        /// The event serialized.
        /// </summary>
        public string Data { get; private set; }

        #region Costructors

        /// <summary>
        /// Initializes a Event Store.
        /// </summary>
        /// <param name="event">The event to store.</param>
        /// <param name="serializedEvent">The event informations serialized.</param>
        public StoredEvent(Event @event, string serializedEvent)
        {
            this.StoredEventId = Guid.NewGuid();
            this.Time = @event.Time;
            this.EventType = @event.EventType;
            this.Data = serializedEvent;
        }

        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}