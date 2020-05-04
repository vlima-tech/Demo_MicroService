
using System;
using System.Collections.Concurrent;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;
using Praticis.Framework.Worker.Internal;

namespace Praticis.Framework.Worker.Core
{
    public class WorkAnalyzer : IWorkAnalyzer
    {
        private readonly ConcurrentDictionary<Type, object> _analyzers;
        private readonly IServiceProvider _serviceProvider;

        public WorkAnalyzer(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._analyzers = new ConcurrentDictionary<Type, object>();
        }

        public QueueType WhereEnqueue<TWork>(TWork work) where TWork : IWork
        {
            var workType = work.GetType();

            var analyzer = (WorkConfigurationWrapper)this._analyzers.GetOrAdd(
                workType,
                t => Activator.CreateInstance(
                    typeof(WorkConfigurationImpl<>).MakeGenericType(workType),
                    this._serviceProvider
                )
            );

            return analyzer.Analyze(work);
        }
    }
}