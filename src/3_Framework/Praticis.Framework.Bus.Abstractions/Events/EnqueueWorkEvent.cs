
using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Bus.Abstractions
{
    public class EnqueueWorkEvent : Event
    {
        public IWork Work { get; private set; }

        public EnqueueWorkEvent(IWork work)
            : base(EventType.Work_Created, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Work = work;
        }
    }
}