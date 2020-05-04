
using System.Collections.Generic;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions.Enums;
using Praticis.Framework.Worker.Abstractions;

namespace Praticis.Framework.Bus.Kafka.Abstractions.Settings
{
    public class KafkaConsumerOption : IKafkaConsumerOption
    {
        public IEnumerable<string> Topics { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<string> Brokers { get; set; }
        public FilterStrategy FilterStrategy { get; set; }
        public IEnumerable<EventType> EventTypes { get; set; }
        public IQueueOption Queue { get; set; }
    }
}