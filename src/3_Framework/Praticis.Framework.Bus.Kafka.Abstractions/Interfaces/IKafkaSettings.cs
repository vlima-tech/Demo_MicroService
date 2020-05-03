
using System.Collections.Generic;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions.Settings;

namespace Praticis.Framework.Bus.Kafka.Abstractions
{
    public interface IKafkaSettings
    {
        public IEnumerable<EventType> IgnoredEvents { get; set; }
        public IEnumerable<string> Brokers { get; set; }
        public IEnumerable<Filter> Producers { get; set; }
        public IEnumerable<Filter> Consumers { get; set; }

        public bool IgnoreEvent(EventType type);
    }
}