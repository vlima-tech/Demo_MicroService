
using System;
using System.Linq;
using System.Linq.Expressions;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions.Enums;
using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Bus.Kafka.Abstractions
{
    public static class ConsumerOptionSpec
    {
        public static Expression<Func<IKafkaConsumerOption, bool>> IsStrategyOfConsumer(QueueType queue, FilterStrategy strategy, EventType eventType)
        {
            return o => o.Queue.QueueId == queue && o.FilterStrategy == strategy && o.EventTypes.Contains(eventType);
        }
    }
}