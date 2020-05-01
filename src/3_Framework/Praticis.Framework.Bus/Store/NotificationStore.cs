
using System;
using System.Linq;
using System.Collections.Generic;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Bus.Store
{
    public class NotificationStore : INotificationStore
    {
        private readonly List<Notification> _notifications;
        private readonly List<Warning> _warnings;
        private readonly List<SystemError> _systemErrors;
        private readonly List<Log> _logs;

        public NotificationStore(List<Notification> notifications, List<Warning> warnings, 
            List<SystemError> systemErrors, List<Log> logs)
        {
            this._notifications = notifications;
            this._warnings = warnings;
            this._systemErrors = systemErrors;
            this._logs = logs;
        }

        /// <summary>
        /// Obtains the notification messages of the application.
        /// </summary>
        /// <returns>
        /// Returns a collection of notification messages.
        /// </returns>
        public IEnumerable<Notification> GetNotifications() => this._notifications;

        /// <summary>
        /// Obtains the system error messages of the application.
        /// </summary>
        /// <returns>
        /// Returns a collection of system error messages.
        /// </returns>
        public IEnumerable<SystemError> GetSystemErrors() => this._systemErrors;

        /// <summary>
        /// Obtains the warning messages of the application.
        /// </summary>
        /// <returns>
        /// Returns a collection of warning messages.
        /// </returns>
        public IEnumerable<Warning> GetWarnings() => this._warnings;

        /// <summary>
        /// Obtains the log messages of the application.
        /// </summary>
        /// <returns>
        /// Returns a collection of log messages.
        /// </returns>
        public IEnumerable<Log> GetLogs() => this._logs;

        public IEnumerable<Notification> GetAll(bool includeNotifications = true, bool includeWarnings = true, bool includeSystemErrors = true, bool includeLogs = true)
        {
            List<Notification> collection = new List<Notification>();

            if (includeNotifications)
                collection.AddRange(this._notifications);

            if (includeWarnings)
                collection.AddRange(this._warnings);

            if (includeSystemErrors)
                collection.AddRange(this._systemErrors);

            if (includeLogs)
                collection.AddRange(this._logs);

            collection = collection.OrderBy(n => n.Time)
                .ToList();

            return collection;
        }

        public IEnumerable<Notification> Find(Func<Notification, bool> predicate, bool includeNotifications = true, bool includeWarnings = true, bool includeSystemErrors = true, bool includeLogs = true)
        {
            List<Notification> collection = new List<Notification>();

            if (includeNotifications)
                collection.AddRange(this._notifications.Where(predicate));

            if (includeWarnings)
                collection.AddRange(this._warnings.Where(predicate));

            if (includeSystemErrors)
                collection.AddRange(this._systemErrors.Where(predicate));

            if (includeLogs)
                collection.AddRange(this._logs.Where(predicate));

            collection = collection.OrderBy(n => n.Time)
                .ToList();

            return collection;
        }

        /// <summary>
        /// Verify if exists notifications in notification store. By default, verify notifications and system errors.
        /// Use the parameters to customise analysis filters.
        /// </summary>
        /// <param name="includeNotifications">Consider notifications to verify if has notifications.</param>
        /// <param name="includeWarnings">Consider warnings to verify if has notifications.</param>
        /// <param name="includeSystemErrors">Consider system errors to verify if has notifications.</param>
        /// <returns>
        /// Return 'True' if exists some notification or 'False' to don't.
        /// </returns>
        public bool HasNotifications(bool includeNotifications = true, bool includeWarnings = false, bool includeSystemErrors = true)
        {
            bool hasNotifications = false;

            if (includeNotifications && !hasNotifications)
                hasNotifications = this._notifications.Count > 0;

            if (includeWarnings && !hasNotifications)
                hasNotifications = this._warnings.Count > 0;

            if (includeSystemErrors && !hasNotifications)
                hasNotifications = this._systemErrors.Count > 0;

            return hasNotifications;
        }

        /// <summary>
        /// Verify if exists log messages in notification store.
        /// </summary>
        /// <returns>
        /// Return 'True' if exists some log or 'False' to don't.
        /// </returns>
        public bool HasLogs() => this._logs.Count > 0;

        public bool HasSystemErrors() => this._systemErrors.Count > 0;
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}