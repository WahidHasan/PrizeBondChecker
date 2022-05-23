using static PrizeBondChecker.Domain.Enums.EnumPrizebond;

namespace PrizeBondChecker.Domain.Prizebond
{
    public class PrizebondCreateModel
    {
        public string serial { get; set; }

        public string bondId { get; set; }
        public DateTime? entryDate { get; set; }
        public CheckBond? Checked { get; set; }
    }
}
