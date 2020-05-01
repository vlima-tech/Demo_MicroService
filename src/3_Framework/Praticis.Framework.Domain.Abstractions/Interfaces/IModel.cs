
using System;
using System.Diagnostics.CodeAnalysis;

using FluentValidation.Results;

namespace Praticis.Framework.Layers.Domain.Abstractions
{
    public interface IModel : IIdentity, IDisposable
    {
        /// <summary>
        /// Verify if the Entity is Valid.
        /// </summary>
        /// <returns>
        /// Return True if Entity is Valid or False if does not valid.
        /// </returns>
        bool IsValid { get; }

        /// <summary>
        /// Contains possibles Collection Notification Domain. Use the "IsValid" Property to
        /// verify if all right or get the notifications to analise messages.
        /// </summary>
        ValidationResult Notifications { get; }

        /// <summary>
        /// Copy property values of the current object to another object.
        /// </summary>
        /// <param name="model">The model of destination changes.</param>
        /// <param name="changeId">Set 'True' to change Id or 'False' to do not change.</param>
        void CopyTo([NotNull] IModel model, bool changeId = false);
    }
}