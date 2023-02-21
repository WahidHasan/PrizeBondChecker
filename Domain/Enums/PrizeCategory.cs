using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum PrizeCategory
    {
        [Description("1st")]
        First = 1,
        [Description("2nd")]
        Second = 2,
        [Description("3rd")]
        Third = 3,
        [Description("4th")]
        Fourth = 4,
        [Description("5th")]
        Fifth = 5
    }
}
