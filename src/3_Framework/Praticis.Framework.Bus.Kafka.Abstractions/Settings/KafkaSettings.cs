
using System.Collections.Generic;
using System.Linq;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Kafka.Abstractions.Settings
{
    public class KafkaSettings : IKafkaSettings
    {
        #region Properties

        public IEnumerable<string> Brokers { get; set; }

        public IEnumerable<EventType> IgnoredEvents { get; set; }

        public IEnumerable<Filter> Producers { get; set; }

        public IEnumerable<Filter> Consumers { get; set; }

        #endregion

        public bool IgnoreEvent(EventType eventType) => this.IgnoredEvents.Contains(eventType);
    }
}