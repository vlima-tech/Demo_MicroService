
using System;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions.Repositories;

namespace Praticis.Framework.Worker.Handlers.Events
{
    public class WorkStartedEventHandler : IEventHandler<WorkStartedEvent>
    {
        private readonly IWorkRepository _workRepository;

        public WorkStartedEventHandler(IServiceProvider serviceProvider)
        {
            this._workRepository = serviceProvider.GetService<IWorkRepository>();
        }

        public async Task Handle(WorkStartedEvent notification, CancellationToken cancellationToken)
        {
            var work = notification.Work;

            work.StartProcessWhen(DateTime.Now);

            await _workRepository.SaveAsync(work);
            await _workRepository.CommitAsync();
        }

        public void Dispose()
        {
            
        }
    }
}