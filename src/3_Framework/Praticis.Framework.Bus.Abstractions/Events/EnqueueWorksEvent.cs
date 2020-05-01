
using System.Collections.Generic;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Bus.Abstractions.Events
{
    public class EnqueueWorksEvent : Event
    {
        public IEnumerable<IWork> Works { get; private set; }

        public EnqueueWorksEvent(IEnumerable<IWork> works)
            : base(EventType.Work_Created, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Works = works;
        }
    }
}