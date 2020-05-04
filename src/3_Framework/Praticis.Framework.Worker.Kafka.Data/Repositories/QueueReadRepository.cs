
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Confluent.Kafka;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;
using Praticis.Framework.Worker.Abstractions.Repositories;
using Praticis.Framework.Worker.Kafka.Data.Context;

namespace Praticis.Framework.Worker.Kafka.Data.Repositories
{
    public class QueueReadRepository : IQueueReadRepository
    {
        private IConsumer<KafkaKey, IWork> _consumer { get; set; }

        public QueueReadRepository(KafkaConsumerContext context, QueueType queue)
        {
            this._consumer = context.GenerateConsumer(queue);
        }

        public Task<IEnumerable<Work>> DequeueWorksAsync(int dequeueLength, CancellationToken cancellationToken)
        {
            IList<Work> works = new List<Work>();
            IList<Task> tasks = new List<Task>();
            ConsumeResult<KafkaKey, IWork> result;
            int count = 0;

            try
            {
                do
                {
                    
                    var conf = new ConsumerConfig
                    {
                        GroupId = "test-consumer-group_005",
                        BootstrapServers = "localhost:9092",
                        AutoOffsetReset = AutoOffsetReset.Earliest
                    };

                    var builder = new ConsumerBuilder<KafkaKey, IWork>(conf);
                    var deserializer = new KafkaConsumerDeserializer();
                    builder.SetKeyDeserializer(deserializer);
                    builder.SetValueDeserializer(deserializer);

                    using (var c = builder.Build())
                    {
                        c.Subscribe("registered-customers");
                        var cr = c.Consume(cancellationToken);

                    }
                    
                        /*
                        var task = Task.Run(() => this._consumer.Consume(cancellationToken));
                        task.ContinueWith(r => ++count);
                        */

                    result = this._consumer.Consume(cancellationToken);
                    
                    //if(result.Message.Key.EventType)

                } while (!result.IsPartitionEOF || count < dequeueLength);
            }
            catch(Exception e)
            {

            }

            return Task.FromResult(works.AsEnumerable());
        }
    }
}