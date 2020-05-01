
using System;
using System.Collections.Generic;

using MediatR;

using ConferIR.Framework.Bus;
using ConferIR.Framework.Bus.Abstractions;
using ConferIR.Framework.Bus.Abstractions.Events;
using ConferIR.Framework.Bus.Abstractions.ValueObjects;
using ConferIR.Framework.Bus.Handlers;
using ConferIR.Framework.Bus.Store;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceBusBootStrapper
    {
        public static void AddServiceBusModule(this IServiceCollection services)
        {
            // Add Mediator Service
            
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());


            // Service Bus Core

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
            //services.AddScoped<IServiceBus, ServiceBus>();


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
    }
}