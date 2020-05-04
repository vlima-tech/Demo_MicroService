
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using Praticis.Framework.Bus;
using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Bus.Abstractions.ValueObjects;
using Praticis.Framework.Bus.Handlers;
using Praticis.Framework.Bus.Store;
using Praticis.Framework.Bus.Kafka.Abstractions;
using Praticis.Framework.Bus.Kafka.Abstractions.Collections;
using Praticis.Framework.Bus.Kafka.Handlers;
using Praticis.Framework.Worker.Application.Commands;
using Praticis.Framework.Bus.Kafka.Abstractions.Settings;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceBusBootStrapper
    {
        public static void AddServiceBusModule(this IServiceCollection services)
        {
            // Add Mediator Service
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            assemblies.Add(typeof(KafkaNotificationEventHandler).Assembly);
            services.AddMediatR(assemblies.ToArray());
            
            // Service Bus Core
            /*
            services.AddScoped<IServiceBus>(serviceProvider =>
            {
                var provider = serviceProvider.CreateScope().ServiceProvider;
                return new ServiceBus
                (
                    provider.GetService<IMediator>(), 
                    provider.GetService<IEventStore>(), 
                    provider.GetService<INotificationStore>()
                );
            });
            */
            services.AddScoped<IServiceBus, ServiceBus>();


            // Event Sourcing
            services.AddScoped<IEventStore, EventStore>();


            // Notification Store
            services.AddScoped<List<Log>>();
            services.AddScoped<List<Notification>>();
            services.AddScoped<List<Warning>>();
            services.AddScoped<List<SystemError>>();
            services.AddScoped<INotificationStore, NotificationStore>();


            // Handlers
            services.AddScoped<INotificationHandler<Log>, NotificationHandler>();
            services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();
            services.AddScoped<INotificationHandler<Warning>, NotificationHandler>();
            services.AddScoped<INotificationHandler<SystemError>, NotificationHandler>();
        }

        public static void AddKafkaBusModule(this IServiceCollection services)
        {
            services.AddSingleton<IKafkaProducerOptions>(provider =>
            {
                var config = new List<KafkaProducerOption>();

                provider.GetService<IConfiguration>().GetSection("Kafka:Producers").Bind(config);

                return new KafkaProducerOptionCollection(config);
            });

            services.AddScoped<IProducer<KafkaKey, IWork>>(provider =>
            {
                var config = new ProducerConfig { };
                var producerBuilder = new ProducerBuilder<KafkaKey, IWork>(config);

                producerBuilder.SetKeySerializer(new ProducerSerializer());
                producerBuilder.SetValueSerializer(new ProducerSerializer());
                
                return producerBuilder.Build();
            });
        }

        private class ProducerSerializer : IAsyncSerializer<IWork>, IAsyncSerializer<KafkaKey>
        {
            public async Task<byte[]> SerializeAsync(KafkaKey data, SerializationContext context)
            {
                var json = Task.Run(() => JsonConvert.SerializeObject(data));

                return Encoding.Default.GetBytes(await json);
            }

            public async Task<byte[]> SerializeAsync(IWork data, SerializationContext context)
            {
                var json = Task.Run(() => JsonConvert.SerializeObject(data));

                return Encoding.Default.GetBytes(await json);
            }
        }
    }
}