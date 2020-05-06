
using System;
using System.Collections.Generic;

using MediatR;
using Microsoft.Extensions.Configuration;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions;
using Praticis.Framework.Bus.Kafka.Abstractions.Collections;
using Praticis.Framework.Bus.Kafka.Abstractions.Enums;
using Praticis.Framework.Bus.Kafka.Abstractions.Settings;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Abstractions.Settings;
using Praticis.Framework.Worker.Application.Commands;
using Praticis.Framework.Worker.Core;
using Praticis.Framework.Worker.Handlers.Commands;
using Praticis.Framework.Worker.Kafka.Data.Context;
using Praticis.Framework.Worker.Kafka.Data.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WorkerBootStrapper
    {
        public static void AddWorkerModule(this IServiceCollection services)
        {
            // Worker (Queue Manage Core)

            services.AddScoped<IWorkAnalyzer, WorkAnalyzer>();
            services.AddSingleton<IWorker, WorkerHostedService>(serviceProvider =>
            {
                IServiceProvider provider;
                IList<QueueHostedService> queues = new List<QueueHostedService>();

                var queueSettings = serviceProvider.GetService<IConfiguration>()
                    .GetQueueSettings();

                foreach (var queueSetting in queueSettings)
                {
                    provider = serviceProvider.CreateScope().ServiceProvider;
                    queues.Add(new QueueHostedService(provider.GetService<IServiceBus>(), new QueueReadRepository(provider.GetService<KafkaConsumerContext>(), queueSetting.QueueId), queueSetting));
                }
                
                return new WorkerHostedService(queues);
            });


            // Commands
            services.AddScoped<IRequestHandler<TestCommand, bool>, TestCommandHandler>();
        }

        public static void AddKafkaWorkerModule(this IServiceCollection services)
        {
            services.AddSingleton<IKafkaConsumerOptions>(provider =>
            {
                var config = new List<KafkaConsumerOption>();

                provider.GetService<IConfiguration>().GetSection("Kafka:Consumers").Bind(config);

                config.Add(new KafkaConsumerOption
                {
                    Topics = new List<string> { "registered-customers" },
                    GroupName = "MicroService1_103",
                    Brokers = new List<string> { "localhost:9092" },
                    FilterStrategy = FilterStrategy.Capture_Types,
                    EventTypes = new List<EventType> { EventType.Registered_Customer },
                    Queue = new QueueOption
                    {
                        QueueId = Praticis.Framework.Worker.Abstractions.Enums.QueueType.Registered_Customers,
                        MaxWIP = 2,
                        MaxLength = 1000,
                        ReloadLevel = 100,
                    }
                });

                return new KafkaConsumerOptionCollection(config);
            });

            services.AddScoped<KafkaConsumerContext>();

            //services.AddScoped<IQueueReadRepository, QueueReadRepository>();
        }
    }
}