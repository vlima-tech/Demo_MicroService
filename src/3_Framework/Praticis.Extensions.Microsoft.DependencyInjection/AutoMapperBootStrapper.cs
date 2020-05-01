using AutoMapper;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoMapperBootStrapper
    {
        public static void AddAutoMapperModules(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Singleton);
        }
    }
}
