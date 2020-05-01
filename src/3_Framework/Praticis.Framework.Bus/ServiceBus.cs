
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Commands;
using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.Events;

namespace Praticis.Framework.Bus
{
    public class ServiceBus : IServiceBus
    {
        private readonly IMediator _mediator;
        public INotificationStore Notifications { get; }
        private readonly IEventStore _eventStore;

        public ServiceBus(IMediator mediator, IEventStore eventStore, INotificationStore notificationStore)
        {
            this._mediator = mediator;
            this._eventStore = eventStore;
            this.Notifications = notificationStore;
        }
        
        public async Task PublishEvent<TEvent>(TEvent @event) where TEvent : IEvent, IWork
        {
            /*
            if (@event.EventType.SelectedValue != EventType.Domain_Notification)
                await this.QueueWork(this._eventStore.Store(@event));
            */
            if (@event.ExecutionMode == ExecutionMode.WaitToClose)
                await Task.Run(() => this._mediator.Publish(@event));
            else
                await this.EnqueueWork(@event);
        }

        public async Task SendCommand<TCommand>(TCommand command) where TCommand : ICommand, IWork
        {
            if (command.ExecutionMode == ExecutionMode.WaitToClose)
                await Task.Run(() => this._mediator.Send(command));
            else
                await this.EnqueueWork(command);
        }

        public async Task<TResponse> SendCommand<TResponse>(ICommand<TResponse> command)
        {
            TResponse response = await Task.Run(() => this._mediator.Send(command));
            return response;
        }

        public async Task EnqueueWork(IWork work)
        {
            await Task.Run(() => this._mediator.Publish(new EnqueueWorkEvent(work)));
        }

        public async Task EnqueueWork(IEnumerable<IWork> works)
        {
            await Task.Run(() => this._mediator.Publish(new EnqueueWorksEvent(works)));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}