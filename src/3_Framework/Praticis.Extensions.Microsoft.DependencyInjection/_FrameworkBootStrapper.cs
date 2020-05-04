
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FrameworkBootStrapper
    {
        public static void AddPraticisFramework<TStartup>(this IServiceCollection services, Action<FrameworkOption> options)
        {
            FrameworkOption frameworkOptions = new FrameworkOption();

            options.Invoke(frameworkOptions);
            
            if (frameworkOptions.UseConfigurationModule)
                services.AddConfigurationModule();

            if (frameworkOptions.UseEnvironmentModule)
                services.AddEnvironmentModule();
            
            if (frameworkOptions.UsePipelineModule)
                services.AddPipelineModule();
            
            if (frameworkOptions.UseWorkerModule)
                services.AddWorkerModule();

            if(frameworkOptions.UseAutoMapperModule)
                services.AddAutoMapperModules();

            if (frameworkOptions.UseKafkaBusModule)
                services.AddKafkaBusModule();

            if (frameworkOptions.UseKafkaWorkerModule)
                services.AddKafkaWorkerModule();

            services.AddServiceBusModule();
        }
    }
}