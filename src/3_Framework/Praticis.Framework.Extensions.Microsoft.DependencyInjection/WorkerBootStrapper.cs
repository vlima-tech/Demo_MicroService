
using System;
using System.Collections.Generic;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ConferIR.Framework.Bus.Abstractions;
using ConferIR.Framework.Worker.Abstractions;
using ConferIR.Framework.Worker.Abstractions.Repositories;
using ConferIR.Framework.Worker.Application.Commands;
using ConferIR.Framework.Worker.Application.ViewModels;
using ConferIR.Framework.Worker.Core;
using ConferIR.Framework.Worker.Data.Context;
using ConferIR.Framework.Worker.Data.Repositories;
using ConferIR.Framework.Worker.Handlers.Commands;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WorkerBootStrapper
    {
        public static void AddWorkerModule(this IServiceCollection services)
        {
            // Work Repository
            
            services.AddDbContext<Worker_Context>((serviceProvider, op) =>
            {
                var conn = serviceProvider.GetService<IConfiguration>()
                    .GetConnectionString("ConferIR_Connection");

                op.UseSqlServer(conn);
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            services.AddTransient<IWorkRepository, WorkRepository>();
            services.AddTransient<IWorkReadRepository, WorkRepository>();


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

            services.AddScoped<IRequestHandler<LoadWorksCommand, WorksPaginatedViewModel>, LoadWorksCommandHandler>();
            services.AddScoped<IRequestHandler<TestCommand, bool>, TestCommandHandler>();
            services.AddScoped<IRequestHandler<LoadAllWorksCommand, List<WorkViewModel>>, LoadAllWorksCommandHandler>();


            // Events

            //services.AddScoped<INotificationHandler<PipelineFinishedEvent>, SavePipelineEventHandler>();
            //services.AddTransient<INotificationHandler<WorkFinishedEvent>, WorkFinishedEventHandler>();
        }
    }
}