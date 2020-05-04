
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Praticis.Framework.Worker.Abstractions.Repositories
{
    public interface IQueueReadRepository
    {
        Task<IEnumerable<Work>> DequeueWorksAsync(int count, CancellationToken cancellationToken);
    }
}