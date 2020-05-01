
using System;
using System.Linq;
using System.Reflection;

using ConferIR.Framework.Worker.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoScanBootStrapper
    {
        /// <summary>
        /// Scan all assembly application and try connect specific abstractions in your concretions classes.
        /// **Obs** Do not functional, still need finally feature.
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoScanModule(this IServiceCollection services)
        {
            var multiOpenInterfaces = new[]
            {
                typeof(IWorkConfiguration<>),
            };

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("ConferIR.Shared.Infra.Server.IoC")).ToList();

            var todosTipos = assemblies[0].GetReferencedAssemblies().SelectMany(ra => Assembly.Load(ra).GetTypes()).ToList();
            // Teste
            var tipo = todosTipos.Where(t => t.Name.Contains("SincronizarDeclaracaoWorkMap")).ToList();
            var tipos = todosTipos.Where(t => t.ImplementsGenericInterface(multiOpenInterfaces[0])).ToList();

            foreach (var multiOpenInterface in multiOpenInterfaces)
            {
                var concretions = todosTipos
                    .Where(type => type.ImplementsGenericInterface(multiOpenInterface))
                    .ToList();

                foreach (var type in concretions)
                {
                    services.AddScoped(multiOpenInterface, type);
                }
            }
        }

        private static bool ImplementsGenericInterface(this Type type, Type interfaceType)
            => type.IsGenericType(interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => @interface.IsGenericType(interfaceType));

        private static bool IsGenericType(this Type type, Type genericType)
            => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
    }
}