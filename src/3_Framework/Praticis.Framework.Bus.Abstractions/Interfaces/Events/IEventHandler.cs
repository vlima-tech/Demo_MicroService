
using System;

using MediatR;

namespace Praticis.Framework.Bus.Abstractions.Events
{
    /// <summary>
    /// The executor of event.
    /// </summary>
    /// <typeparam name="TEvent">The event.</typeparam>
    public interface IEventHandler<TEvent> : INotificationHandler<TEvent>, IDisposable
        where TEvent : IEvent
    {

    }
}