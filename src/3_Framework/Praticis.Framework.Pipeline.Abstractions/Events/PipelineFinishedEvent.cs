
using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Commands;
using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Pipeline.Abstractions.Events
{
    public class PipelineFinishedEvent : Event
    {
        public readonly string PipelineLog;
        public readonly ICommand Request;
        public readonly INotificationStore NotificationStore;

        public PipelineFinishedEvent(ICommand request, INotificationStore notificationStore, string pipelineLog)
            : base(EventType.Pipeline_Finished, ExecutionMode.WaitToClose, EventStoreMode.InMemory)
        {
            this.Request = request;
            this.PipelineLog = pipelineLog;
            this.NotificationStore = notificationStore;
        }
    }
}