
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Confluent.Kafka;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Bus.Abstractions.ValueObjects;
using Praticis.Framework.Bus.Kafka.Abstractions;

namespace Praticis.Framework.Bus.Kafka.Handlers
{
    public class KafkaNotificationEventHandler : IEventHandler<EnqueueWorkEvent>
    {
        private readonly IServiceBus _serviceBus;
        private readonly IKafkaProducerOptions _kafkaOptions;
        private IProducer<KafkaKey, IWork> _producer { get; set; }
        
        public KafkaNotificationEventHandler(IServiceProvider provider)
        {
            this._serviceBus = provider.GetService<IServiceBus>();
            this._kafkaOptions = provider.GetService<IKafkaProducerOptions>();
            this._producer = provider.GetService<IProducer<KafkaKey, IWork>>();
        }

        public async Task Handle(EnqueueWorkEvent notification, CancellationToken cancellationToken)
        {
            var producers = this._kafkaOptions.IdentifyListeners(notification.EventType);

            if (!producers.Any())
                return;

            var tasks = new List<Task>();

            try
            {
                var msg = notification.GenerateMessage();

                producers.SelectMany(o => o.Brokers)
                    .ToList()
                    .ForEach(broker => this._producer.AddBrokers(broker));

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