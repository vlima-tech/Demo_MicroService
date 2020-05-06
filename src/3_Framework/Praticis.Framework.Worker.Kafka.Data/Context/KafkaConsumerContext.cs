
using System;
using System.Linq;
using System.Text;

using Confluent.Kafka;
using Newtonsoft.Json;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Kafka.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Worker.Kafka.Data.Context
{
    public class KafkaConsumerContext
    {
        public IKafkaConsumerOptions Options { get; private set; }

        public KafkaConsumerContext(IKafkaConsumerOptions options)
        {
            this.Options = options;
        }

        public IConsumer<KafkaKey, IWork> GenerateConsumer(QueueType queue)
        {
            var deserializer = new KafkaConsumerDeserializer();

            var options = this.Options.Where(o => o.Queue.QueueId == queue);

            if(options.Select(c => c.GroupName).Distinct().Count() != 1)
            {
                // TODO: Sent System Error notification
            }

            var config = new ConsumerConfig
            {
                GroupId = options.First().GroupName,
                BootstrapServers = string.Join(';', options.SelectMany(o => o.Brokers)),
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var builder = new ConsumerBuilder<KafkaKey, IWork>(config);

            builder.SetKeyDeserializer(deserializer);
            builder.SetValueDeserializer(deserializer);

            var container = builder.Build();
            
            var topics = options.SelectMany(o => o.Topics);

            container.Subscribe(topics);

            return container;
        }
    }

    public class KafkaConsumerDeserializer : IDeserializer<KafkaKey>, IDeserializer<IWork>
    {
        private KafkaKey _key;

        public KafkaKey Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.Default.GetString(data.ToArray());

            this._key = JsonConvert.DeserializeObject<KafkaKey>(json);

            return this._key;
        }
        
        IWork IDeserializer<IWork>.Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.Default.GetString(data.ToArray());
            
            var work = JsonConvert.DeserializeObject(json, this._key.Type) as IWork;

            return work;
        }
    }
}