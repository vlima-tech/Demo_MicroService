
using System;
using System.ComponentModel.DataAnnotations;

using Praticis.Framework.Worker.Abstractions.Enums;

namespace Praticis.Framework.Worker.Application.ViewModels
{
    public class WorkViewModel
    {
        [Display(Name = "Id", ResourceType = typeof(Resources.WorkViewModelResource))]
        public Guid Id { get; private set; }

        [Display(Name = "QueueId", ResourceType = typeof(Resources.WorkViewModelResource))]
        public QueueType QueueId { get; private set; }

        [Display(Name = "TaskId", ResourceType = typeof(Resources.WorkViewModelResource))]
        public int TaskId { get; private set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.WorkViewModelResource))]
        public string Name { get; private set; }

        [Display(Name = "CreationDate", ResourceType = typeof(Resources.WorkViewModelResource))]
        public DateTime CreationDate { get; private set; }

        [Display(Name = "EnqueueDate", ResourceType = typeof(Resources.WorkViewModelResource))]
        public DateTime? EnqueueDate { get; private set; }

        [Display(Name = "StartProcessDate", ResourceType = typeof(Resources.WorkViewModelResource))]
        public DateTime? StartProcessDate { get; private set; }

        [Display(Name = "FinishProcessDate", ResourceType = typeof(Resources.WorkViewModelResource))]
        public DateTime? FinishProcessDate { get; private set; }
        
        [Display(Name = "Status", ResourceType = typeof(Resources.WorkViewModelResource))]
        public WorkStatus Status { get; private set; }

        [Display(Name = "Data", ResourceType = typeof(Resources.WorkViewModelResource))]
        public string Data { get; private set; }

        [Display(Name = "ExecutionLog", ResourceType = typeof(Resources.WorkViewModelResource))]
        public string ExecutionLog { get; private set; }
    }
}