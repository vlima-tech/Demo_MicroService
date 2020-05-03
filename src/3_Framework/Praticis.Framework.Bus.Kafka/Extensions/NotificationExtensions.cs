
using System;
using System.Collections.Generic;

using Confluent.Kafka;

using Praticis.Framework.Bus.Abstractions.Enums;

namespace Praticis.Framework.Bus.Abstractions.ValueObjects
{
    public static class NotificationExtensions
    {
        public static Message<KeyValuePair<EventType, Type>, IWork> GenerateMessage(this Event notification)
        {
            var key = notification.EventType;
            var type = notification.ResourceType;

            return new Message<KeyValuePair<EventType, Type>, IWork>
            {
                Key = new KeyValuePair<EventType, Type>(key, type),
                Value = notification
            };
        }
    }
}