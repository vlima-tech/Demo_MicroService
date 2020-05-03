
using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ProjectName.Customers.Application.Commands._1._0.CustomerCommands;
using ProjectName.Customers.Application.ViewModels._1._0;

namespace ProjectName.Web.API.Service1.Controllers
{
    [Route("api/[Controller]")]
    public class CustomersController : BaseController
    {
        public CustomersController(IServiceProvider provider)
            : base(provider)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customer = new CustomerViewModel
            {
                Name = "João Batista",
                BirthDate = new DateTime(1980, 04, 20),
                Address = new AddressViewModel
                {
                    Street = "Rua dos Testes Manuais",
                    Number = "205"
                }
            };

            customer = new CustomerViewModel
            {
                Name = "Luiz Felipe Canassa",
                BirthDate = new DateTime(1992, 07, 28),
                Address = new AddressViewModel
                {
                    Street = "Rua dos Inconfidentes",
                    Number = "50"
                }
            };

            customer = new CustomerViewModel
            {
                Name = "Valdir da Silva Valdruga",
                BirthDate = new DateTime(1988, 01, 05),
                Address = new AddressViewModel
                {
                    Street = "Rua das Caminhadas",
                    Number = "2560"
                }
            };

            customer = new CustomerViewModel
            {
                Name = "João Molevade",
                BirthDate = new DateTime(1965, 10, 12),
                Address = new AddressViewModel
                {
                    Street = "Rua Ouro Preto",
                    Number = "100"
                }
            };

            await this.ServiceBus.SendCommand(new SaveCustomerCommand(customer));

            return Response();
        }
    }
}