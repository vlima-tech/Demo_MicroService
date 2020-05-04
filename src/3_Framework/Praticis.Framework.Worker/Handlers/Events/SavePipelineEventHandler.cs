
using System;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Pipeline.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions.Enums;
using Praticis.Framework.Worker.Abstractions.Repositories;

namespace Praticis.Framework.Worker.Handlers.Events
{
    public class SavePipelineEventHandler : IEventHandler<PipelineFinishedEvent>
    {
        private IWorkRepository _workRepository;

        public SavePipelineEventHandler(IServiceProvider serviceProvider)
        {
            this._workRepository = serviceProvider.GetService<IWorkRepository>();
        }

        public async Task Handle(PipelineFinishedEvent notification, CancellationToken cancellationToken)
        {
            if (!(notification.Request is IWork))
                return;

            var work = _workRepository?.SearchByIdAsync(notification.Request.CommandId)
                .GetAwaiter()
                .GetResult();

            if(work is null)
            {
                // TODO: implementar envio de notificação sobre não ter encontrado
                return;
            }

            if (notification.NotificationStore.HasNotifications())
                work.ChangeStatusTo(WorkStatus.Failed);
            else
                work.ChangeStatusTo(WorkStatus.Processed);

            work.DefineExecutionLog(notification.PipelineLog);

            await this._workRepository.SaveAsync(work);
            await this._workRepository.CommitAsync();
        }

        public void Dispose()
        {
            this._workRepository?.Dispose();
            this._workRepository = null;
        }
    }
}