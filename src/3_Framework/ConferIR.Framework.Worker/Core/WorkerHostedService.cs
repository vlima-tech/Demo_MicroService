
using System.Collections.Generic;
using System.Threading.Tasks;

using Praticis.Framework.Worker.Abstractions;

namespace Praticis.Framework.Worker.Core
{
    public class WorkerHostedService : List<QueueHostedService>, IWorker
    {
        public WorkerHostedService(IEnumerable<QueueHostedService> queues)
            => base.AddRange(queues);

        public async Task InitializeAsync()
        {
            foreach (var item in this)
                await item.InitializeAsync();
        }

        public void Dispose()
        {

        }
    }
}