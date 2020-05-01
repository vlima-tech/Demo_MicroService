
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Server.Data;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Abstractions.Repositories;
using Praticis.Framework.Worker.Data.Context;

namespace Praticis.Framework.Worker.Data.Repositories
{
    public class WorkRepository : BaseRepository<Work>, IWorkRepository, IWorkReadRepository
    {
        public WorkRepository(Worker_Context context, IServiceProvider serviceProvider) 
            : base(context, serviceProvider)
        {
        }

        public Task<Work> SearchByTaskIdAsync(int taskId)
        {
            var work = base.Db.Set<Work>()
                .Where(WorkSpec.SearchByTaskId(taskId))
                .FirstOrDefault();
            
            return Task.FromResult(work);
        }

        public async Task<bool> AnyAsync<TRequest>(Expression<Func<Work, bool>> workPredicate, Expression<Func<TRequest, bool>> requestPredicate)
            where TRequest : IWork
        {
            bool any = false;
            int page = 1, pageSize = 500;
            IList<Work> works = null;
            Func<TRequest, bool> match;

            match = requestPredicate.Compile();

            do
            {
                works?.Clear();
                works = null;

                works = await this.FindAsync(workPredicate, page, pageSize);
                any = works.Any(w => w.RequestType == typeof(TRequest) && match(w.GetRequest<TRequest>()));

                page++;
            } while (any == false && works.Count > 0);
            
            return any;
        }
    }
}