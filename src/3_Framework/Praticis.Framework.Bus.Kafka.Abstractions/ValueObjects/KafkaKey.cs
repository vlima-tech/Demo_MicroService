
using System;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Kafka.Abstractions
{
    public class KafkaKey
    {
        public EventType EventType { get; set; }
        public Type Type { get; set; }

        #region Constructors

        protected KafkaKey()
        {

        }

        public KafkaKey(EventType eventType, Type type)
        {
            this.EventType = eventType;
            this.Type = type;
        }

        #endregion
    }
}