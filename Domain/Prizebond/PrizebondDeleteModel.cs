using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prizebond
{
    public class PrizebondDeleteModel
    {
        public Guid UserId { get; set; }
        public List<string> BondIds { get; set; }
    }
}
