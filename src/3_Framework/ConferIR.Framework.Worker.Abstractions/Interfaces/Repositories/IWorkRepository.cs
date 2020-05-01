
using Praticis.Framework.Layers.Data.Abstractions;

namespace Praticis.Framework.Worker.Abstractions.Repositories
{
    public interface IWorkRepository : IBaseRepository<Work>, IWorkReadRepository
    {

    }
}