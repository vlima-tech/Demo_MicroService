
using System;
using System.Linq.Expressions;

using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Worker.Abstractions
{
    public static class WorkSpec
    {
        public static Expression<Func<Work, bool>> SearchByTaskId(int taskId)
        {
            return w => w.TaskId == taskId;
        }

        public static Expression<Func<Work, bool>> LoadWorks(QueueType queue)
        {
            return w => w.QueueId == queue;
        }

        public static Expression<Func<Work, bool>> LoadWorks(QueueType queue, WorkStatus status)
        {
            return w => w.QueueId == queue && w.Status == status;
        }
    }
}