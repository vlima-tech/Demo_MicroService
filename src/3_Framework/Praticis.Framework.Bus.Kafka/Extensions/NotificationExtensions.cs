
using Confluent.Kafka;

using Praticis.Framework.Bus.Kafka.Abstractions;

namespace Praticis.Framework.Bus.Abstractions.ValueObjects
{
    public static class NotificationExtensions
    {
        public static Message<KafkaKey, IWork> GenerateMessage(this EnqueueWorkEvent notification)
        {
            var eventType = notification.EventType;
            var workType = notification.Work.GetType();

            return new Message<KafkaKey, IWork>
            {
                Key = new KafkaKey(eventType, workType),
                Value = notification.Work
            };
        }
    }
}