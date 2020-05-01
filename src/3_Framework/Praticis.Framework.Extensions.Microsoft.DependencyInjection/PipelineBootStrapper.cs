
using MediatR;

using ConferIR.Framework.Pipeline;
using ConferIR.Framework.Pipeline.Abstractions;
using ConferIR.Framework.Pipeline.Abstractions.Events;
using ConferIR.Framework.Pipeline.Collections;
using ConferIR.Framework.Pipeline.Handlers;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class PipelineBootStrapper
    {
        internal static void AddPipelineModule(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LogPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CronosPipeline<,>));   
            // ErrorCatch need be last pipline added
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ErrorCatchPipeline<,>));
            
            services.AddScoped<IPipelineStore, PipelineInfoCollection>();
            services.AddScoped<INotificationHandler<PipelineFinishedEvent>, PipelineFinishedEventHandler>();
        }
    }
}