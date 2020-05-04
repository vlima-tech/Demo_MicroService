
using System;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Worker.Abstractions.Events
{
    public class EnqueuedWorkEvent : Event
    {
        public Work Work { get; private set; }
        public DateTime EnqueueDate { get; private set; }

        public EnqueuedWorkEvent(Work work, DateTime enqueueDate)
            : base(EventType.Work_Finished, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Work = work;
            this.EnqueueDate = enqueueDate;
        }
    }
}