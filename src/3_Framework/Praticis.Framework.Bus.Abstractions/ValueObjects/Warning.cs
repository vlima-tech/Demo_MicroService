
using System;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Abstractions.ValueObjects
{
    public class Warning : Notification
    {
        #region Constructors

        /// <summary>
        /// Initializes a warning notification message.
        /// </summary>
        /// <param name="warningId">The warning id.</param>
        /// <param name="code">The warning code.</param>
        /// <param name="message">The warning message.</param>
        public Warning(Guid warningId, string code, string message)
            : base(warningId, code, message, NotificationType.Warning)
        {

        }

        /// <summary>
        /// Initializes a warning notification message.
        /// </summary>
        /// <param name="code">The warning code.</param>
        /// <param name="message">The warning message.</param>
        public Warning(string code, string message)
            : base(code, message, NotificationType.Warning)
        {

        }

        /// <summary>
        /// Initializes a warning notification message.
        /// </summary>
        /// <param name="message">The warning message.</param>
        public Warning(string message)
            : base(message, NotificationType.Warning)
        {

        }

        #endregion

        public override string ToString() => base.Value;
    }
}