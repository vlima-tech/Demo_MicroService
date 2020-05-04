
using System;

using MediatR;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Abstractions.Events
{
    /// <summary>
    /// Represents an event of application.
    /// </summary>
    public interface IEvent : INotification, IWork
    {
        /// <summary>
        /// The event Id.
        /// </summary>
        Guid EventId { get; }

        /// <summary>
        /// The event name.
        /// </summary>
        string EventName { get; }

        /// <summary>
        /// The time that the event occurred.
        /// </summary>
        DateTime Time { get; }

        /// <summary>
        /// The event store mode.
        /// </summary>
        EventStoreMode StoreMode { get; }

        /// <summary>
        /// The assembly context of the event.
        /// </summary>
        Type ResourceType { get; }
    }
}