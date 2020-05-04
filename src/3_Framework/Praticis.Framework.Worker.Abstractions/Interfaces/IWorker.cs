
using System.Threading.Tasks;

namespace Praticis.Framework.Worker.Abstractions
{
    public interface IWorker //: IDisposable
    {
        /// <summary>
        /// Initialize queues
        /// </summary>
        /// <returns>Returns a completed task.</returns>
        Task InitializeAsync();
    }
}