
namespace Microsoft.Extensions.DependencyInjection
{
    public static class CustomerBootStrapper
    {
        public static void AddCustomerModule(this IServiceCollection services)
        {
            services.AddCustomerCommands();
        }
    }
}