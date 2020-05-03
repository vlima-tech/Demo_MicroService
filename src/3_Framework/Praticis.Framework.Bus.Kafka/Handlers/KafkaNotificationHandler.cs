
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Confluent.Kafka;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Bus.Abstractions.ValueObjects;
using Praticis.Framework.Bus.Kafka.Abstractions;
using Praticis.Framework.Bus.Kafka.Abstractions.Settings;

namespace Praticis.Framework.Bus.Kafka.Handlers
{
    public class KafkaNotificationHandler : IEventHandler<Event>
    {
        private readonly IServiceBus _serviceBus;
        private readonly IKafkaSettings _kafkaConfig;
        private IProducer<KeyValuePair<EventType, Type>, IWork> _producer { get; set; }
        
        public KafkaNotificationHandler(IServiceProvider provider)
        {
            this._serviceBus = provider.GetService<IServiceBus>();
            this._kafkaConfig = provider.GetService<IKafkaSettings>();
            this._producer = provider.GetService<IProducer<KeyValuePair<EventType, Type>, IWork>>();
        }

        public async Task Handle(Event notification, CancellationToken cancellationToken)
        {
            if (this._kafkaConfig.IgnoreEvent(notification.EventType))
                return;

            var tasks = new List<Task>();
            var key = notification.EventType;

            try
            {
                var msg = notification.GenerateMessage();

                var producers = this._kafkaConfig.Producers.AsQueryable()
                    .Where(FindProducerSpec.FindProducer(key));

                foreach (var item in producers)
                    tasks.Add(this._producer.ProduceAsync(item.Topic, msg));

                await Task.WhenAll(tasks);
            }
            catch(Exception e)
            {
                var msg = $"An error occurred on sent notification to kafka: {e.Message}";
                await this._serviceBus.PublishEvent(new SystemError(msg));
            }
        }

        public void Dispose()
        {
            
        }
    }
}