
using System.Collections.Generic;

using Praticis.Framework.Worker.Abstractions.Settings;

namespace Praticis.Framework.Worker.Application.ViewModels
{
    public class QueueViewModel
    {
        public List<QueueSetting> Queues { get; set; }
        public List<WorkViewModel> Works { get; set; }
        public int LineQtd { get; set; }
    }
}
