
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FrameworkBootStrapper
    {
        public static void AddPraticisFramework<TStartup>(this IServiceCollection services, Action<FrameworkOption> options)
        {
            FrameworkOption frameworkOptions = new FrameworkOption();

            options.Invoke(frameworkOptions);
            
            if (frameworkOptions.LoadConfigurationModule)
                services.AddConfigurationModule();

            if (frameworkOptions.LoadEnvironmentModule)
                services.AddEnvironmentModule();
            
            if (frameworkOptions.LoadPipelineModule)
                services.AddPipelineModule();
            
            if (frameworkOptions.LoadRESTModule)
                services.AddRESTModule();

            if (frameworkOptions.LoadWorkerModule)
                services.AddWorkerModule();

            if(frameworkOptions.LoadAutoMapperModule)
                services.AddAutoMapperModules();

            //services.AddAutoScanModule();

            services.AddServiceBusModule();
        }
    }
}