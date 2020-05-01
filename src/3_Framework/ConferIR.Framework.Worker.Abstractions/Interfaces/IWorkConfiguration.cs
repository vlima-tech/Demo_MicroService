
using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Worker.Abstractions
{
    public interface IWorkConfiguration<TWork> where TWork : IWork
    {
        QueueType Analyze(TWork work);
    }
}