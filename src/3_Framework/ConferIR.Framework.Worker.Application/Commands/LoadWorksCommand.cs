
using Praticis.Framework.Bus.Abstractions.ValueObjects;
using Praticis.Framework.Worker.Abstractions.Enums;
using Praticis.Framework.Worker.Application.ViewModels;

namespace Praticis.Framework.Worker.Application.Commands
{
    public class LoadWorksCommand : Command<WorksPaginatedViewModel>
    {
        public QueueType Queue { get; private set; }
        
        public int PageIndex { get; private set; }
        
        public int PageSize { get; private set; }

        public bool DescendingOrder { get; private set; }

        public LoadWorksCommand(QueueType queue, int pageIndex = 0, int pageSize = 10, bool descendingOrder = false)
        {
            this.Queue = queue;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.DescendingOrder = descendingOrder;
        }
    }
}