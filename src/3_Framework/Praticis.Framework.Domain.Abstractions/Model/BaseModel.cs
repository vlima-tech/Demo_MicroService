
using System;

using FluentValidation.Results;

namespace Praticis.Framework.Layers.Domain.Abstractions
{
    public abstract class BaseModel<T> : IModel where T : IModel
    {
        public abstract bool IsValid { get; }

        public ValidationResult Notifications { get; protected set; }

        #region Custom Getters and Setters

        public Guid Id { get; protected set; }

        #endregion

        #region Constructors

        protected BaseModel()
        {
            this.Id = Guid.NewGuid();
            this.Notifications = new ValidationResult();
        }

        protected BaseModel(Guid id)
        {
            this.Id = id;
            this.Notifications = new ValidationResult();
        }

        #endregion

        #region Comparer Overrides

        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseModel<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(BaseModel<T> a, BaseModel<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseModel<T> a, BaseModel<T> b)
        { return !(a == b); }

        public override int GetHashCode()
        { return (GetType().GetHashCode() * 907) + Id.GetHashCode(); }

        #endregion

        /// <summary>
        /// Copy property values of the current object to another object.
        /// </summary>
        /// <param name="model">The model of destination changes.</param>
        /// <param name="changeId">Set 'True' to change Id or 'False' to do not change.</param>
        public abstract void CopyTo(IModel model, bool changeId = false);

        public override string ToString()
        { return typeof(BaseModel<>).Name + " [Id = " + Id + "]"; }

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

                this.Notifications?.Errors.Clear();

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

        #endregion
    }

    public abstract class BaseModel : IModel
    {
        #region Custom Getters and Setters

        public Guid Id { get; protected set; }

        public abstract bool IsValid { get; }

        public ValidationResult Notifications { get; protected set; }

        #endregion

        #region Constructors

        protected BaseModel()
        {
            this.Id = Guid.NewGuid();
            this.Notifications = new ValidationResult();
        }

        protected BaseModel(Guid id)
        {
            this.Id = id;
            this.Notifications = new ValidationResult();
        }

        #endregion

        #region Comparer Overrides

        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseModel;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(BaseModel a, BaseModel b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseModel a, BaseModel b)
        { return !(a == b); }

        public override int GetHashCode()
        { return (GetType().GetHashCode() * 907) + Id.GetHashCode(); }

        #endregion

        /// <summary>
        /// Copy property values of the current object to another object.
        /// </summary>
        /// <param name="model">The model of destination changes.</param>
        /// <param name="changeId">Set 'True' to change Id or 'False' to do not change.</param>
        public abstract void CopyTo(IModel model, bool changeId = false);

        public override string ToString()
        { return typeof(BaseModel<>).Name + " [Id = " + Id + "]"; }

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

                this.Notifications?.Errors.Clear();

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

        #endregion
    }
}