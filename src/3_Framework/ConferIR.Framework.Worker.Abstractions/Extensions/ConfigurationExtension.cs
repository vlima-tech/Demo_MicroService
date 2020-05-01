
using System.Collections.Generic;
using System.Linq;

using Praticis.Framework.Worker.Abstractions.Enums;
using Praticis.Framework.Worker.Abstractions.ValueObjects;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtension
    {
        public static IReadOnlyList<QueueSetting> GetQueueSettings(this IConfiguration configuration)
        {
            List<QueueSetting> queueSettings = new List<QueueSetting>();

            // Add queues from appsetings
            configuration.GetSection("Queues").Bind(queueSettings);

            // Add default queue
            if (!queueSettings.Any(q => q.QueueId == QueueType.Default))
                queueSettings.Insert(0,new QueueSetting());

            return queueSettings;
        }
    }
}