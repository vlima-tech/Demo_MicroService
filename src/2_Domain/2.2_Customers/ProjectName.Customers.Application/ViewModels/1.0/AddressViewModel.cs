
using System;

namespace ProjectName.Customers.Application.ViewModels._1._0
{
    public class AddressViewModel
    {
        public Guid AddressId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }

        public AddressViewModel()
        {
            this.AddressId = Guid.NewGuid();
        }
    }
}