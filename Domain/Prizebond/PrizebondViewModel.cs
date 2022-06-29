using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrizeBondChecker.Domain.Enums.EnumPrizebond;

namespace Domain.Prizebond
{
    public class PrizebondViewModel
    {
        public string? serial { get; set; }
        public string? bondId { get; set; }
        public DateTime? entryDate { get; set; }
        public CheckBond? Checked { get; set; }
    }
}
