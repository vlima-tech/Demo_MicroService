
using System;
using System.Collections.Generic;
using System.Linq;
using Confluent.Kafka;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Worker.Kafka.Data.Context
{
    public class KafkaConsumerContext
    {
        protected IKafkaConsumerSettings Options { get; private set; }

        public KafkaConsumerContext(IKafkaConsumerSettings options)
        {
            this.Options = options;
        }

        public IConsumer<KafkaKey, IWork> GenerateConsumer(QueueType queue)
        {
            var config = new ConsumerConfig
            {
                GroupId = this.Options.Group,
                BootstrapServers = string.Join(';', this.Options.Brokers),
            };

            var builder = new ConsumerBuilder<KafkaKey, IWork>(config);
            var container = builder.Build();
            
            var topics = this.Options.Topics.Where(t => t.QueueId == queue)
                .Select(t => t.Title);

            container.Subscribe(topics);

            return container;
        }
    }
}