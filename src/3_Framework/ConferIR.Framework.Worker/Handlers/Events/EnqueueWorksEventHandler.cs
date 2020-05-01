
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Abstractions.Repositories;

namespace Praticis.Framework.Worker.Handlers.Events
{
    public class EnqueueWorksEventHandler : IEventHandler<EnqueueWorksEvent>
    {
        private readonly IWorkRepository _workRepository;
        private readonly IWorkAnalyzer _workAnalyzer;

        public EnqueueWorksEventHandler(IServiceProvider serviceProvider)
        {
            this._workRepository = serviceProvider.GetService<IWorkRepository>();
            this._workAnalyzer = serviceProvider.GetService<IWorkAnalyzer>();
        }

        public async Task Handle(EnqueueWorksEvent notification, CancellationToken cancellationToken)
        {
            List<Task> tasks = new List<Task>();

            if (notification.Works.Count() < 1)
                return;

            foreach(var item in notification.Works)
            {
                var work = new Work(item);

                var queueType = this._workAnalyzer.WhereEnqueue(work.Request);
                work.DefineQueueId(queueType);

                tasks.Add(this._workRepository.SaveAsync(work));
            }

            do
            {
                Thread.Sleep(300);
            } while (tasks.All(t => !t.IsCompleted));

            await this._workRepository.CommitAsync();
        }

        public void Dispose()
        {

        }
    }
}
