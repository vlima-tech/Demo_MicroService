
using System;
using System.Collections.Generic;
using System.Linq;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions.Settings;

namespace Praticis.Framework.Bus.Kafka.Abstractions.Collections
{
    public class KafkaProducerOptionCollection : List<IKafkaProducerOption>, IKafkaProducerOptions
    {
        #region Constructors

        public KafkaProducerOptionCollection()
        { }

        public KafkaProducerOptionCollection(IEnumerable<IKafkaProducerOption> collection)
            => base.AddRange(collection);

        public KafkaProducerOptionCollection(IKafkaProducerOption[] array)
            => base.AddRange(array);

        public KafkaProducerOptionCollection(IEnumerable<KafkaProducerOption> collection)
            => base.AddRange(collection);

        public KafkaProducerOptionCollection(KafkaProducerOption[] array)
            => base.AddRange(array);

        #endregion

        public IEnumerable<IKafkaProducerOption> IdentifyListeners(EventType eventType)
        {
            List<IKafkaProducerOption> collection = new List<IKafkaProducerOption>();

            collection.AddRange(
                this.Where(o => o.FilterStrategy == FilterStrategy.Capture_Types && o.EventTypes.Contains(eventType))
            );

            collection.AddRange(
                this.Where(o => o.FilterStrategy == FilterStrategy.Ignore_Types && !o.EventTypes.Contains(eventType))
            );

            return collection;
        }
    }
}