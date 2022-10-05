using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Models
{
    public class Pager<T>
    {
        public long Count { get; set; }

        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
