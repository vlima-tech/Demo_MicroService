
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Praticis.Framework.Layers.Application.Abstractions
{
    public abstract class BaseViewModel<TViewModel> : IBaseViewModel where TViewModel : IViewModel
    {
        /// <summary>
        /// Verify if ViewModel is Valid.
        /// </summary>
        /// <returns>Return True if ViewModel is Valid or False if not is valid.</returns>
        public virtual bool IsValid() { return true; }

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        {

        }

        /// <summary>
        /// Set a value in property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">Private Property, the source.</param>
        /// <param name="value">The Value to attribute the Property.</param>
        /// <param name="propertyName">The Get Set Method.</param>
        /// <returns></returns>
        public virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }

        #region IDisposable Support

        // Para detectar chamadas redundantes
        private bool isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // Free any other managed objects here.
                }

                // Free any unmanaged objects here.

                isDisposed = true;
            }
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            // No change this Method. Enter the cleaning code in Dispose(bool disposing).
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        ~BaseViewModel()
        {
            Dispose(false);
        }

        #endregion
    }
}