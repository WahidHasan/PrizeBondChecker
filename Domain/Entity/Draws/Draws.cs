using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Draws
{
    public class Draws : BaseEntity
    {
        public List<SelectedBond> SelectedBonds = new List<SelectedBond>();
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime DrawDate { get; set; }
        public int DrawNumber { get; set; }
    }
}
