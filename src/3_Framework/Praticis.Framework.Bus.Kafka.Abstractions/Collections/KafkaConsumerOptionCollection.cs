
using System.Collections.Generic;
using System.Linq;

using Praticis.Framework.Bus.Kafka.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions.Settings;
using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Bus.Kafka.Abstractions.Collections
{
    public class KafkaConsumerOptionCollection : List<IKafkaConsumerOption>, IKafkaConsumerOptions
    {
        #region Constructors

        public KafkaConsumerOptionCollection()
        { }

        public KafkaConsumerOptionCollection(IEnumerable<IKafkaConsumerOption> collection)
            => base.AddRange(collection);

        public KafkaConsumerOptionCollection(IKafkaConsumerOption[] array)
            => base.AddRange(array);

        public KafkaConsumerOptionCollection(IEnumerable<KafkaConsumerOption> collection)
            => base.AddRange(collection);

        public KafkaConsumerOptionCollection(KafkaConsumerOption[] array)
            => base.AddRange(array);

        #endregion

        public bool IsExecutedInQueue(QueueType queue, KafkaKey key)
        {
            bool execute;

            execute = this.AsQueryable().Any(ConsumerOptionSpec.IsStrategyOfConsumer(queue, FilterStrategy.Capture_Types, key.EventType));

            if(!execute)
                execute = this.AsQueryable().Any(ConsumerOptionSpec.IsStrategyOfConsumer(queue, FilterStrategy.Ignore_Types, key.EventType));

            return execute;
        }
    }
}