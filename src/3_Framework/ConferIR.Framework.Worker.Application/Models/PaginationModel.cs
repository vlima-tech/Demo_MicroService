
using System;

namespace Praticis.Framework.Worker.Application.Models
{
    public class PaginationModel
    {        
        public int CurrentPage { get; set; }
        public long Count { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
    }
}