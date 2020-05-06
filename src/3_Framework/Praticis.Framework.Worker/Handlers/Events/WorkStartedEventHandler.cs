
using System;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions.Repositories;

namespace Praticis.Framework.Worker.Handlers.Events
{
    public class WorkStartedEventHandler : IEventHandler<StartedWorkEvent>
    {
        private IWorkRepository _workRepository { get; set; }

        public WorkStartedEventHandler(IServiceProvider serviceProvider)
        {
            this._workRepository = serviceProvider.GetService<IWorkRepository>();
        }

        public Task Handle(StartedWorkEvent notification, CancellationToken cancellationToken)
        {
            var work = notification.Work;

            work.StartProcessWhen(DateTime.Now);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this._workRepository?.Dispose();
            this._workRepository = null;
        }
    }
}