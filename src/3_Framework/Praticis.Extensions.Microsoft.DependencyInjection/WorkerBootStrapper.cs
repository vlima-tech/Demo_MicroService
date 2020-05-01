
using System;
using System.Collections.Generic;

using MediatR;
using Microsoft.Extensions.Configuration;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Application.Commands;
using Praticis.Framework.Worker.Application.ViewModels;
using Praticis.Framework.Worker.Core;
using Praticis.Framework.Worker.Handlers.Commands;

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
                    queues.Add(new QueueHostedService(provider.GetService<IServiceBus>(), provider, queueSetting));
                }
                
                return new WorkerHostedService(queues);
            });


            // Commands

            services.AddScoped<IRequestHandler<TestCommand, bool>, TestCommandHandler>();
            services.AddScoped<IRequestHandler<LoadWorksCommand, WorksPaginatedViewModel>, LoadWorksCommandHandler>();
            services.AddScoped<IRequestHandler<LoadAllWorksCommand, List<WorkViewModel>>, LoadAllWorksCommandHandler>();
        }
    }
}