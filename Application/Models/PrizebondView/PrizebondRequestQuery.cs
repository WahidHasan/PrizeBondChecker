using Application.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.PrizebondView
{
    public class PrizebondRequestQuery : PaginationBase
    {
        public Guid UserId { get; set; }
        public string SearchText { get; set; } = string.Empty;
    }
}
