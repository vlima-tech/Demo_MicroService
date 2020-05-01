
using System;
using System.Linq.Expressions;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Worker.Abstractions.Repositories
{
    public static class FindWorkSpec
    {
        /// <summary>
        /// Specification to find active works. 
        /// Are considered work status: <strong>Created</strong>, <strong>Enqueued</strong>, <strong>Processing</strong>
        /// </summary>
        /// <typeparam name="TRequest">The request of work.</typeparam>
        /// <returns>Returns a function to test each work.</returns>
        public static Expression<Func<Work, bool>> FindActiveWorks<TRequest>()
            where TRequest : IWork
        {
            var requestType = typeof(TRequest);
            return w => w.RequestType == requestType && ((w.Status == WorkStatus.Created || w.Status == WorkStatus.Enqueued || w.Status == WorkStatus.Processing));
        }
    }
}