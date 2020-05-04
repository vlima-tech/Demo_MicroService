
using System;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Worker.Abstractions.Events
{
    public class FinishedWorkEvent : Event
    {
        public Work Work { get; private set; }
        public int TaskId { get; private set; }
        public DateTime FinishedDate { get; private set; }

        public FinishedWorkEvent(Work work, int taskId, DateTime finishedDate)
            : base(EventType.Work_Finished, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Work = work;
            this.TaskId = taskId;
            this.FinishedDate = finishedDate;
        }
    }
}