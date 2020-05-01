
using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Worker.Abstractions
{
    public interface IWorkAnalyzer
    {
        QueueType WhereEnqueue<TWork>(TWork work) where TWork : IWork;
    }
}