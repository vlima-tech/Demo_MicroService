
using System;
using System.Runtime.CompilerServices;

namespace Praticis.Framework.Layers.Application.Abstractions
{
    public interface IBaseViewModel : IDisposable
    {
        /// <summary>
        /// Set a value in property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">Private Property, the source.</param>
        /// <param name="value">The Value to attribute the Property.</param>
        /// <param name="propertyName">The Get Set Method.</param>
        /// <returns></returns>
        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null);

        /// <summary>
        /// Verify if ViewModel is Valid.
        /// </summary>
        /// <returns>Return True if ViewModel is Valid or False if not is valid.</returns>
        bool IsValid();
    }
}