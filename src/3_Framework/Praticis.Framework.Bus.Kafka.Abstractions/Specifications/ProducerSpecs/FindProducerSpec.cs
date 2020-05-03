
using System;
using System.Linq;
using System.Linq.Expressions;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Kafka.Abstractions.Settings
{
    public static class FindProducerSpec
    {
        public static Expression<Func<Filter, bool>> FindProducer(EventType eventType)
            => p => p.EventTypes.Contains(eventType);
    }
}