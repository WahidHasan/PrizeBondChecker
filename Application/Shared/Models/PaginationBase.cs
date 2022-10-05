using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Models
{
    public class PaginationBase
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
