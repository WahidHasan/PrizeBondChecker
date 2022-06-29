using Domain.Prizebond;

namespace PrizeBondChecker.Domain.Prizebond
{
    public class PrizebondCreateModel
    {
        public Guid UserId { get; set; }
        public List<PrizebondViewModel> Prizebonds { get; set; }
    }
}
