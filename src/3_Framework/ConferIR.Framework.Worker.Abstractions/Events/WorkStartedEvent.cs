
using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Worker.Abstractions.Events
{
    public class WorkStartedEvent : Event
    {
        public Work Work { get; private set; }

        public WorkStartedEvent(Work work)
            : base(EventType.Work_Started, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Work = work;
        }
    }
}
