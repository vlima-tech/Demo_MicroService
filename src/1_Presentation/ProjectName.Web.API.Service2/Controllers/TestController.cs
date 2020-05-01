
using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Worker.Application.Commands;

namespace ProjectName.Web.API.Service2.Controllers
{
    [Route("api/[Controller]")]
    public class TestController : BaseController
    {
        public TestController(IServiceProvider provider)
            : base(provider)
        {

        }

        public async Task<IActionResult> Index(TimeSpan? time, bool enqueue = false)
        {
            var executionTime = time ?? TimeSpan.FromMilliseconds(new Random().Next(100, 30000));
            var cmd = new TestCommand(executionTime);

            if (!enqueue)
                cmd.ChangeExecutionMode(ExecutionMode.WaitToClose);

            await this.ServiceBus.SendCommand(cmd);

            return Response();
        }
    }
}