
using System;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Worker.Internal
{
    internal abstract class WorkConfigurationWrapper
    {
        public abstract QueueType Analyze(object work);
    }

    internal class WorkConfigurationImpl<TWork> : WorkConfigurationWrapper where TWork : IWork
    {
        private readonly IWorkConfiguration<TWork> _workConfig;

        public WorkConfigurationImpl(IServiceProvider serviceProvider)
        {
            this._workConfig = serviceProvider.GetService<IWorkConfiguration<TWork>>();
        }

        public override QueueType Analyze(object work)
        {
            try
            {
                if (this._workConfig is null)
                    return QueueType.Default;

                return this._workConfig.Analyze((TWork)work);
            }
            catch(Exception e)
            {
                return QueueType.Default;
            }
        }
    }
}