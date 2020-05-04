
using System;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions.Repositories;

namespace Praticis.Framework.Worker.Handlers.Events
{
    public class WorkFinishedEventHandler : IEventHandler<FinishedWorkEvent>
    {
        private readonly IWorkRepository _workRepository;

        public WorkFinishedEventHandler(IServiceProvider serviceProvider)
        {
            this._workRepository = serviceProvider.GetService<IWorkRepository>();
        }

        public async Task Handle(FinishedWorkEvent notification, CancellationToken cancellationToken)
        {
            var work = await this._workRepository.SearchByTaskIdAsync(notification.TaskId);
            
            work.FinishProcessWhen(notification.FinishedDate);

            await _workRepository.SaveAsync(work);
            await _workRepository.CommitAsync();
        }

        public void Dispose()
        {

        }
    }
}