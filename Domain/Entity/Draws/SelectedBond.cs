using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Draws
{
    public class SelectedBond
    {
        public string BondId { get; set; }
        public PrizeCategory PrizeCategory { get; set; }
    }
}
