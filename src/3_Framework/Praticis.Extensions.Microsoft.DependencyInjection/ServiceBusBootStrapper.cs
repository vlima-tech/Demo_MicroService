
using System;
using System.Collections.Generic;

using MediatR;

using Praticis.Framework.Bus;
using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Bus.Abstractions.ValueObjects;
using Praticis.Framework.Bus.Handlers;
using Praticis.Framework.Bus.Store;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceBusBootStrapper
    {
        public static void AddServiceBusModule(this IServiceCollection services)
        {
            // Add Mediator Service
            
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            
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
    }
}