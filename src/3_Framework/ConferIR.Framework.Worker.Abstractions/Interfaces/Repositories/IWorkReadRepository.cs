
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Layers.Data.Abstractions;

namespace Praticis.Framework.Worker.Abstractions.Repositories
{
    public interface IWorkReadRepository : IBaseReadRepository<Work>
    {
        /// <summary>
        /// Search work by task id.
        /// </summary>
        /// <param name="taskId">The work task id</param>
        /// <returns>Return a work if exists or null to do not exists.</returns>
        Task<Work> SearchByTaskIdAsync(int taskId);

        /// <summary>
        /// Verify existing works that match with work predicate and then analyze request predicate parameter.
        /// Recommended include in work predicate analyze request type. Use: var exists = await AnyAsync(w => w.RequestType == typeof(Request) && ...)
        /// </summary>
        /// <typeparam name="TRequest">The command.</typeparam>
        /// <param name="workPredicate">A function to test each work for a condition.</param>
        /// <param name="requestPredicate">A function to test each request for a condition.</param>
        /// <returns>Returns 'True' if found or 'False' to not found.</returns>
        Task<bool> AnyAsync<TRequest>(Expression<Func<Work, bool>> workPredicate, Expression<Func<TRequest, bool>> requestPredicate)
            where TRequest : IWork;
    }
}