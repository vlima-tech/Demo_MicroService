
using System;
using System.Threading.Tasks;

namespace Praticis.Framework.Bus.Abstractions.Events
{
    /// <summary>
    /// The mechanism that orchestrate the events.
    /// </summary>
    public interface IEventStore : IDisposable
    {
        /// <summary>
        /// Store the event informations in StorageMode property of event.
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task<bool> Store(IEvent @event);
    }
}