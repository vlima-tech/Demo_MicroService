
using System;

using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Worker.Abstractions.Settings
{
    public class QueueOption : IQueueOption
    {
        public QueueType QueueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxWIP { get; set; }
        public int MaxLength { get; set; }
        public int ReloadLevel { get; set; }
        public TimeSpan ReloadInterval { get; set; }
        public TimeSpan EnqueueProcessTimeout { get; set; }
        public TimeSpan SleepInterval { get; set; }

        /// <summary>
        /// Create a default queue
        /// </summary>
        public QueueOption()
        {
            this.QueueId = QueueType.Default;
            this.Name = "Default";
            this.Description = "Default Queue";
            this.MaxWIP = 5;
            this.MaxLength = 200;
            this.ReloadLevel = 50;
            this.ReloadInterval = new TimeSpan(0, 3, 0);
            this.EnqueueProcessTimeout = new TimeSpan(0, 1, 0);
            this.SleepInterval = new TimeSpan(0, 0, 5);
        }

        /// <summary>
        /// Create a queue
        /// </summary>
        public QueueOption(QueueType queueType, string name, string description, 
            int maxWip, int maxLength, int reloadLevel, TimeSpan reloadInterval,
            TimeSpan enqueueProcessTimeout, TimeSpan sleepInterval)
        {
            this.QueueId = queueType;
            this.Name = name;
            this.Description = description;
            this.MaxWIP = maxWip;
            this.MaxLength = maxLength;
            this.ReloadLevel = reloadLevel;
            this.ReloadInterval = reloadInterval;
            this.EnqueueProcessTimeout = enqueueProcessTimeout;
            this.SleepInterval = sleepInterval;
        }
    }
}