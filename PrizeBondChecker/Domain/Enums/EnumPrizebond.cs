using System.ComponentModel;

namespace PrizeBondChecker.Domain.Enums
{
    public class EnumPrizebond
    {
        public enum CheckBond
        {
            [Description("Checked")]
            Checked,
            [Description("Unchecked")]
            Unchecked
        }
    }
}
