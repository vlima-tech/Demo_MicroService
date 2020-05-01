
using Microsoft.Extensions.Hosting;

using ConferIR.Framework.Environment;
using ConferIR.Framework.Environment.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class EnvironmentBootStrapper
    {
        internal static void AddEnvironmentModule(this IServiceCollection services, System.Action<Environment> options)
        {
            var env = new Environment();
            
            options.Invoke(env);

            services.AddSingleton<IEnvironment>(env);
        }
        
        internal static void AddEnvironmentModule(this IServiceCollection services)
        {
            var env = new Environment();
            
            services.AddSingleton<IEnvironment>((container) => 
            {
                var env = container.GetService<IHostEnvironment>();

                return new Environment(env.EnvironmentName);
            });
        }
        
    }
}