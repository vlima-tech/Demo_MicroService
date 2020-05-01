
using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Pipeline.Abstractions;

namespace Praticis.Framework.Pipeline
{
    public class CronosPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IPipelineStore _pipelineStore;

        public CronosPipeline(IServiceProvider serviceProvider)
        {
            this._pipelineStore = serviceProvider.GetService<IPipelineStore>();
        }
        
        public async Task<TResponse> Handle(TRequest command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            int commandHash;
            DateTime startTime, finishTime;

            commandHash = command.GetHashCode();

            startTime = DateTime.Now;

            var result = await next();

            finishTime = DateTime.Now;

            this._pipelineStore.Add(new PipelineInfo(commandHash, startTime, finishTime));

            return result;
        }
    }
}