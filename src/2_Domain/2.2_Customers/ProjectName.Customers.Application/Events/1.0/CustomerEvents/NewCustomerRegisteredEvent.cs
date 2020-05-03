
using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

using ProjectName.Customers.Application.ViewModels._1._0;

namespace ProjectName.Customers.Application.Events._1._0.CustomerEvents
{
    public class NewCustomerRegisteredEvent : Event
    {
        public CustomerViewModel Customer { get; private set; }

        public NewCustomerRegisteredEvent(CustomerViewModel customer)
            : base(EventType.Registered_Customer, ExecutionMode.WaitToClose, EventStoreMode.CloudStore)
        {
            this.Customer = customer;
        }
    }
}