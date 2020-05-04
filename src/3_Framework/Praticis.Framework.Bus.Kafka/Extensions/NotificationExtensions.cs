
using Confluent.Kafka;

using Praticis.Framework.Bus.Kafka.Abstractions;

namespace Praticis.Framework.Bus.Abstractions.ValueObjects
{
    public static class NotificationExtensions
    {
        public static Message<KafkaKey, IWork> GenerateKafkaMessage(this IWork work)
        {
            var eventType = work.EventType;
            var workType = work.GetType();

            return new Message<KafkaKey, IWork>
            {
                Key = new KafkaKey(eventType, workType),
                Value = work
            };
        }
    }
}