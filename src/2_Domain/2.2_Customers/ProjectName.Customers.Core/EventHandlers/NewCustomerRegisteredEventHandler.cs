
using System;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Events;

using ProjectName.Customers.Application.Events._1._0.CustomerEvents;

namespace ProjectName.Customers.Core.EventHandlers
{
    public class NewCustomerRegisteredEventHandler : IEventHandler<NewCustomerRegisteredEvent>
    {
        private readonly IServiceBus _serviceBus;

        public NewCustomerRegisteredEventHandler(IServiceProvider provider)
        {
            this._serviceBus = provider.GetService<IServiceBus>();
        }

        public Task Handle(NewCustomerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}