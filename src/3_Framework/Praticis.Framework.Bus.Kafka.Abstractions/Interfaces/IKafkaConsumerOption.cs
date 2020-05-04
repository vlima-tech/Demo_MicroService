
using System.Collections.Generic;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions.Enums;
using Praticis.Framework.Worker.Abstractions;
namespace Praticis.Framework.Bus.Kafka.Abstractions
{
    public interface IKafkaConsumerOption
    {
        IEnumerable<string> Topics { get; }
        string GroupName { get; }
        IEnumerable<string> Brokers { get; }
        FilterStrategy FilterStrategy { get; }
        IEnumerable<EventType> EventTypes { get; }
        IQueueOption Queue { get; }
    }
}