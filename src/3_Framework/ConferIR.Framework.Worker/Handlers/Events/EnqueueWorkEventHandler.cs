
using System;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Abstractions.Repositories;

namespace Praticis.Framework.Worker.Handlers.Events
{
    public class EnqueueWorkEventHandler : IEventHandler<EnqueueWorkEvent>
    {
        private readonly IWorkRepository _workRepository;
        private readonly IWorkAnalyzer _workAnalyzer;

        public EnqueueWorkEventHandler(IServiceProvider serviceProvider)
        {
            this._workRepository = serviceProvider.GetService<IWorkRepository>();
            this._workAnalyzer = serviceProvider.GetService<IWorkAnalyzer>();
        }

        public async Task Handle(EnqueueWorkEvent notification, CancellationToken cancellationToken)
        {
            var work = new Work(notification.Work);

            var queueType = this._workAnalyzer.WhereEnqueue(work.Request);
            work.DefineQueueId(queueType);

            await this._workRepository.SaveAsync(work);
            await this._workRepository.CommitAsync();
        }

        public void Dispose()
        {
            
        }
    }
}