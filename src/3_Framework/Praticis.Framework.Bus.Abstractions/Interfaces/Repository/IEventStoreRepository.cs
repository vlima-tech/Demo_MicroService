
using System;
using System.Collections.Generic;

using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Bus.Abstractions.Repository
{
    public interface IEventStoreRepository : IDisposable
    {
        /// <summary>
        /// Store an event.
        /// </summary>
        /// <param name="storedEvent">The event to store.</param>
        void Store(StoredEvent storedEvent);

        /// <summary>
        /// Obtains all events of the an entity id.
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <returns></returns>
        ICollection<StoredEvent> All(Guid aggregateId);

        /// <summary>
        /// Filters a sequence of events based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ICollection<StoredEvent> Find(Predicate<Func<StoredEvent, bool>> predicate);
    }
}