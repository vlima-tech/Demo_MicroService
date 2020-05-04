
using System;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Commands;
using Praticis.Framework.Bus.Abstractions.ValueObjects;
using Praticis.Framework.Worker.Application.Commands;

namespace Praticis.Framework.Worker.Handlers.Commands
{
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        private readonly IServiceBus _serviceBus;

        public TestCommandHandler(IServiceProvider provider)
        {
            this._serviceBus = provider.GetService<IServiceBus>();
        }

        public async Task<bool> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            await this._serviceBus.PublishEvent(new Log("Starting Process..."));

            await this._serviceBus.PublishEvent(
                new Log(string.Format("Simulating a execution of {0} ms", request.SleepInterval.TotalMilliseconds))
            );

            Thread.Sleep(request.SleepInterval);

            await this._serviceBus.PublishEvent(new Log("Process executed successfully!!"));

            return true;
        }

        public void Dispose()
        {

        }
    }
}