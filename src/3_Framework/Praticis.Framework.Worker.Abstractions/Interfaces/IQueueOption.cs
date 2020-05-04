
using System;

using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Worker.Abstractions
{
    public interface IQueueOption
    {
        QueueType QueueId { get; }
        string Name { get; }
        string Description { get; }
        int MaxWIP { get; }
        int MaxLength { get; }
        int ReloadLevel { get; }
        TimeSpan ReloadInterval { get; }
        TimeSpan EnqueueProcessTimeout { get; }
        TimeSpan SleepInterval { get; }
    }
}