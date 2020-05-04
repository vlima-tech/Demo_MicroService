
using System.Collections.Generic;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions.Enums;

namespace Praticis.Framework.Bus.Kafka.Abstractions.Settings
{
    public class KafkaProducerOption : IKafkaProducerOption
    {
        public string Topic { get; set; }
        public IEnumerable<string> Brokers { get; set; }
        public FilterStrategy FilterStrategy { get; set; }
        public IEnumerable<EventType> EventTypes { get; set; }
    }
}