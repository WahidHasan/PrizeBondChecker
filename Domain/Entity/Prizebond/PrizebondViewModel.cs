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
        public string? Serial { get; set; }
        public string? BondId { get; set; }
        public string? BondIdInBengali { get; set; }
        public DateTime? EntryDate { get; set; }
        public CheckBond? Checked { get; set; }
    }
}
