
using System.Collections.Generic;

using Praticis.Framework.Bus.Kafka.Abstractions.Settings;

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
    }
}