using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.TestModel
{
    public class Bond
    {
        public int BondId { get; set; }
        public string Serial { get; set; } = string.Empty;
        public DateTime CollectionDate { get; set; }
    }
}
