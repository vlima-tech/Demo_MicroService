
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