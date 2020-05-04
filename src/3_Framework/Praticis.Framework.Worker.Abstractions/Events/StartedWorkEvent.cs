
using System;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Worker.Abstractions.Events
{
    public class StartedWorkEvent : Event
    {
        public Work Work { get; private set; }
        public int TaskId { get; private set; }
        public DateTime StartDate { get; private set; }

        public StartedWorkEvent(Work work, int taskId, DateTime startDate)
            : base(EventType.Work_Started, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Work = work;
            this.TaskId = taskId;
            this.StartDate = startDate;
        }
    }
}
