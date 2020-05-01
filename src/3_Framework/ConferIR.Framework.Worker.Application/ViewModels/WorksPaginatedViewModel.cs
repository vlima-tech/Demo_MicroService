using Praticis.Framework.Worker.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Praticis.Framework.Worker.Application.ViewModels
{
    public class WorksPaginatedViewModel : PaginationModel
    {
        public List<WorkViewModel> Works { get; set; }

        public WorksPaginatedViewModel(IEnumerable<WorkViewModel> works, long count, int currentPage, int pageSize)
        {
            this.Works = works.ToList();
            this.Count = count;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
        }
    }
}
