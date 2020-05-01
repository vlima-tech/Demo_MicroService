
using MediatR;

using Praticis.Framework.Pipeline;
using Praticis.Framework.Pipeline.Abstractions;
using Praticis.Framework.Pipeline.Collections;

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
        }
    }
}