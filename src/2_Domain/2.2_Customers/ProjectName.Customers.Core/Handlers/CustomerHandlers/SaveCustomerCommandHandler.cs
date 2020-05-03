
using System;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Commands;

using ProjectName.Customers.Application.Commands._1._0.CustomerCommands;
using ProjectName.Customers.Application.Events._1._0.CustomerEvents;

namespace ProjectName.Customers.Core.Handlers.CustomerHandlers
{
    public class SaveCustomerCommandHandler : ICommandHandler<SaveCustomerCommand>
    {
        private readonly IServiceBus _serviceBus;

        public SaveCustomerCommandHandler(IServiceProvider provider)
        {
            this._serviceBus = provider.GetService<IServiceBus>();
        }

        public async Task<bool> Handle(SaveCustomerCommand request, CancellationToken cancellationToken)
        {
            bool success;

            // simulate handler works
            Thread.Sleep(1000);

            await this._serviceBus.PublishEvent(new NewCustomerRegisteredEvent(request.Customer));

            success = this._serviceBus.Notifications.HasNotifications();

            return success;
        }

        public void Dispose()
        {
            
        }
    }
}