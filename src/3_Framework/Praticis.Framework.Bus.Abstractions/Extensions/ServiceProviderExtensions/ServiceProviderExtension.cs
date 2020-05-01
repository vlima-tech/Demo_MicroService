
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class ServiceProviderExtension
    {
        /// <summary>
        /// Get an object instance from dependency injection container.
        /// User 'CreateNewInstance' parameter to obtains new instance, still having existing scoped instance in DI stack.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceLocator">The dependency injection container.</param>
        /// <param name="createNewInstance"> Set 'True' to create new instance, still having existing Scoped instance will be returned new object.</param>
        /// <returns></returns>
        public static T GetService<T>(this IServiceProvider serviceLocator, bool createNewInstance = false)
        {
            T instance;

            try
            {
                if (createNewInstance)
                    instance = serviceLocator.TryGetSetvice<T>();
                else
                    instance = (T)serviceLocator.GetService(typeof(T));
            }
            catch(Exception e)
            {
                instance = serviceLocator.TryGetSetvice<T>();
            }

            return instance;
        }
        
        private static T TryGetSetvice<T>(this IServiceProvider serviceLocator)
        {
            T instance;

            try
            {
                instance = (T)serviceLocator.CreateScope()
                    .ServiceProvider
                    .GetService(typeof(T));
            }
            catch(Exception e)
            {
                instance = default;
            }

            return instance;
        }
    }
}