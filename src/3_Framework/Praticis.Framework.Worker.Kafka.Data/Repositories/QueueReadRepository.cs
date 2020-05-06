
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Confluent.Kafka;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Kafka.Abstractions;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;
using Praticis.Framework.Worker.Abstractions.Repositories;
using Praticis.Framework.Worker.Kafka.Data.Context;

namespace Praticis.Framework.Worker.Kafka.Data.Repositories
{
    public class QueueReadRepository : IQueueReadRepository
    {
        private readonly IKafkaConsumerOptions _consumerOptions;
        private QueueType _queue;
        private IConsumer<KafkaKey, IWork> _consumer { get; set; }
        
        public QueueReadRepository(KafkaConsumerContext context, QueueType queue)
        {
            this._queue = queue;
            this._consumer = context.GenerateConsumer(queue);
            this._consumerOptions = context.Options;
        }

        public Task<IEnumerable<Work>> DequeueWorksAsync(int dequeueLength, CancellationToken cancellationToken)
        {
            var tasks = new List<Task<ConsumeResult<KafkaKey, IWork>>>();
            bool repeat = true;

            try
            {
                do
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    var task = Task.Run(() => this._consumer.Consume(TimeSpan.FromSeconds(5)));

                    tasks.Add(task);

                    if(tasks.Count >= dequeueLength || tasks.Any(t => t.Status == TaskStatus.RanToCompletion && t.Result is null))
                    {
                        Task.WhenAll(tasks);

                        repeat = !tasks.Any(t => t.Result is null);

                        var compatibleTasks = tasks.Where(t => t.Result != null && this._consumerOptions.IsExecutedInQueue(this._queue, t.Result.Message.Key));

                        tasks.RemoveAll(t => !compatibleTasks.Contains(t));
                    }

                } while (repeat && tasks.Count < dequeueLength);
            }
            catch(Exception e)
            {

            }

            return Task.FromResult(tasks.Select(t => t.Result).Select(r => new Work(r.Message.Value)));
        }
    }
}