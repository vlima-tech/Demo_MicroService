
using System;

namespace Praticis.Framework.Bus.Abstractions.Enums
{
    /// <summary>
    /// An Event can be serialized and the informations stored.
    /// Event Store Mode define the store type.
    /// </summary>
    [Flags]
    public enum EventStoreMode
    {
        /// <summary>
        /// Informations will be stored in memory.
        /// </summary>
        InMemory = 1,

        /// <summary>
        /// Informations will be stored in local database.
        /// </summary>
        LocalStore = 2,

        /// <summary>
        /// Informations will be sent to QUEUE Cloud Store, if configured.
        /// </summary>
        CloudStore = 3,
    }
}