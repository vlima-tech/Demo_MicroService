
using System.Collections.Generic;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions.Enums;

namespace Praticis.Framework.Bus.Kafka.Abstractions
{
    public interface IKafkaProducerOption
    {
        string Topic { get; }
        IEnumerable<string> Brokers { get; }
        FilterStrategy FilterStrategy { get; }
        IEnumerable<EventType> EventTypes { get; }
    }
}