
using Microsoft.Extensions.Configuration;

using ConferIR.Framework.Environment.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ConfigurationBootStrapper
    {
        internal static void AddConfigurationModule(this IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>((serviceProvider) =>
            {
                var env = serviceProvider.GetService<IEnvironment>();
                
                var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ConfigurationRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{ env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                var teste = builder.Build();
                return builder.Build();
            });
        }

        internal static void AddConfigurationModule(this IServiceCollection services, string environmentName, string configurationPath)
        {
            services.AddSingleton<IConfiguration>((serviceProvider) =>
            {
                var env = serviceProvider.GetService<IEnvironment>();

                var builder = new ConfigurationBuilder()
                    .SetBasePath(configurationPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{ environmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                return builder.Build();
            });
        }
    }
}