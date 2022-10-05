using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prizebond
{
    public class PrizebondDeleteModel
    {
        public PrizebondDeleteModel()
        {
            BondIds = new();
        }
        public Guid UserId { get; set; }
        public List<Guid> BondIds { get; set; }
    }
}
