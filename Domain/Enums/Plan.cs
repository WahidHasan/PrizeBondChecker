using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum Plan
    {
        [Description("Free")]
        Free = 50,
        [Description("Gold")]
        Gold = 100,
        [Description("Unlimited")]
        Unlimited = 500
    }
}
