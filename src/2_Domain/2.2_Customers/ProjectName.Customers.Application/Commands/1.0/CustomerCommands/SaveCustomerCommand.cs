
using Praticis.Framework.Bus.Abstractions.ValueObjects;
using ProjectName.Customers.Application.ViewModels._1._0;

namespace ProjectName.Customers.Application.Commands._1._0.CustomerCommands
{
    public class SaveCustomerCommand : Command
    {
        public CustomerViewModel Customer { get; private set; }

        public SaveCustomerCommand(CustomerViewModel customer)
        {
            this.Customer = customer;
        }
    }
}