
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ProjectNameBootStrapper
    {
        public static void AddProjectNameModules(this IServiceCollection services)
        {
            services.AddCustomerModule();
        }
    }
}