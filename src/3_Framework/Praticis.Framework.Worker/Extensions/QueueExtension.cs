using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class QueueExtension
    {
        public static bool CanExecuteNewTask(this List<Task> _tasksInExecution, int maxWip)
        {
            return _tasksInExecution.Count < maxWip;
        }
    }
}