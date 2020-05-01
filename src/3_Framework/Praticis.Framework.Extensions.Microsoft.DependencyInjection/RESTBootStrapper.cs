

using ConferIR.Framework.REST.Abstractions;
using ConferIR.Framework.REST.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RESTBootStrapper
    {
        public static void AddRESTModule(this IServiceCollection services)
        {
            services.AddScoped<IRequestService, RequestService>();
        }
    }
}