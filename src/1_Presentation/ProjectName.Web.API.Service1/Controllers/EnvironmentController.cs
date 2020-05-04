
using System;
using System.Threading;

using Microsoft.AspNetCore.Mvc;

using Praticis.Framework.Environment.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;
using Praticis.Framework.Worker.Kafka.Data.Context;
using Praticis.Framework.Worker.Kafka.Data.Repositories;

namespace ProjectName.Web.API.Service1.Controllers
{
    [Route("api/[Controller]")]
    public class EnvironmentController : BaseController
    {
        private readonly IEnvironment _environment;
        private readonly IServiceProvider provider;
        public EnvironmentController(IServiceProvider provider)
            : base(provider)
        {
            this._environment = provider.GetService<IEnvironment>();
            this.provider = provider;
        }

        public IActionResult Index()
        {
            //var queue = new QueueReadRepository(provider.GetService<KafkaConsumerContext>(), QueueType.Registered_Customers);

            //queue.DequeueWorksAsync(1, CancellationToken.None);

            return Json(this._environment);
        }
    }
}