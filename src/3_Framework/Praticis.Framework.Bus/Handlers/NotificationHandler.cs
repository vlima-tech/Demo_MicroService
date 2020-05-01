
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Bus.Handlers
{
    public class NotificationHandler : IEventHandler<Notification>
    {
        private readonly List<Notification> _notifications;
        private readonly List<Warning> _warnings;
        private readonly List<SystemError> _systemErrors;
        private readonly List<Log> _logs;

        public NotificationHandler(IServiceProvider serviceProvider)
        {
            this._notifications = serviceProvider.GetService<List<Notification>>();
            this._warnings = serviceProvider.GetService<List<Warning>>();
            this._systemErrors = serviceProvider.GetService<List<SystemError>>();
            this._logs = serviceProvider.GetService<List<Log>>();
        }
        
        public Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            switch (notification.NotificationType)
            {
                case NotificationType.Notification:
                    this._notifications?.Add(notification);
                    break;

                case NotificationType.Warning:
                    this._warnings?.Add(notification as Warning);
                    break;

                case NotificationType.System_Error:
                    this._systemErrors?.Add(notification as SystemError);
                    break;

                case NotificationType.Log:
                    this._logs?.Add(notification as Log);
                    break;
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}