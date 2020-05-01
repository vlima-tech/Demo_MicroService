
using Microsoft.Extensions.Hosting;

namespace Praticis.Framework.Environment.Abstractions
{
    public interface IEnvironment : IHostEnvironment
    {
        string ApplicationRootPath { get; }

        string ConfigurationRootPath { get; }

        bool IsDevelopment();

        bool IsEnvironment(string environmentName);

        bool IsProduction();

        bool IsStaging();
    }
}