using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Draw
{
    public class PrizeTypeViewModel
    {
        [DisplayName("1st")]
        public string First { get; set; }

        [DisplayName("2nd")]
        public Guid Second { get; set; }

        [DisplayName("3rd")]
        public string Third { get; set; }

        [DisplayName("4th")]
        public Guid Fourth { get; set; }

        [DisplayName("5th")]
        public string Fifth { get; set; }
    }
}
