
using System.Collections.Generic;
using System.Linq;

using Praticis.Framework.Worker.Abstractions.Enums;
using Praticis.Framework.Worker.Abstractions.Settings;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtension
    {
        public static IReadOnlyList<QueueOption> GetQueueSettings(this IConfiguration configuration)
        {
            List<QueueOption> queueSettings = new List<QueueOption>();

            // Add queues from appsetings
            configuration.GetSection("Queues").Bind(queueSettings);
            /*
            // Add default queue
            if (!queueSettings.Any(q => q.QueueId == QueueType.Default))
                queueSettings.Insert(0,new QueueOption());
                */
            return queueSettings;
        }
    }
}