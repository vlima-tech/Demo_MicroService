
using System.Collections.Generic;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Kafka.Abstractions
{
    public interface IKafkaProducerOptions : IEnumerable<IKafkaProducerOption>
    {
        public IEnumerable<IKafkaProducerOption> IdentifyReceivers(EventType eventType);
    }
}