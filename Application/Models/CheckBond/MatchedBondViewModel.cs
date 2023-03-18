using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CheckBond
{
    public class MatchedBondViewModel
    {
        public string Serial { get; set; }
        public string BondId { get; set; }
        public string? BondIdInBengali { get; set; }
        public PrizeCategory PrizeCategory { get; set; }
        public string Notes { get; set; }
    }
}
