
using System;

namespace ProjectName.Customers.Application.ViewModels._1._0
{
    public class CustomerViewModel
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public AddressViewModel Address { get; set; }

        public CustomerViewModel()
        {
            this.CustomerId = Guid.NewGuid();
        }
    }
}