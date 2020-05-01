
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Pipeline
{
    public class ErrorCatchPipeline<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    {
        private readonly IServiceBus _serviceBus;

        public ErrorCatchPipeline(IServiceBus serviceBus)
        {
            this._serviceBus = serviceBus;
        }

        public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> response)
        {
            TResponse result = default;

            try
            {
                result = await response();
            }
            catch (Exception e)
            {
                string stackLine, sourceFileName, sourceMethod;
                int errorLine = 0;

                stackLine = e.StackTrace.Split('\n').First();

                var sourcePaths = stackLine.Substring(0, stackLine.IndexOf('(')).Split('.');

                int startIndex = stackLine.IndexOf("in") + 3;
                int endIndex = stackLine.IndexOf("line") - startIndex - 1;

                if (endIndex > 0)
                    sourceFileName = stackLine.Substring(startIndex, endIndex).Trim();
                else
                    sourceFileName = stackLine;

                sourceMethod = sourcePaths.Last();

                int.TryParse(stackLine.Substring(stackLine.IndexOf("line") + 5).Trim(), out errorLine);

                await this._serviceBus.PublishEvent(
                    new SystemError(command, e.Message, e.StackTrace, e.InnerException?.Message, sourceMethod, errorLine, sourceFileName)
                );
            }

            return result;
        }
    }
}