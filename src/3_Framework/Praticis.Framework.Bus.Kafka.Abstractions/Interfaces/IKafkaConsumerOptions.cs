
using System.Collections.Generic;

using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Bus.Kafka.Abstractions
{
    public interface IKafkaConsumerOptions : IEnumerable<IKafkaConsumerOption>
    {
        bool IsExecutedInQueue(QueueType queue, KafkaKey key);
    }
}