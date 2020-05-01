using Praticis.Framework.Worker.Abstractions.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Praticis.Framework.Worker.Application.ViewModels
{
    public class QueueViewModel
    {
        public List<QueueSetting> Queues { get; set; }
        public List<WorkViewModel> Works { get; set; }
        public int LineQtd { get; set; }
    }
}
