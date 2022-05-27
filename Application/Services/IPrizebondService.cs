using PrizeBondChecker.Domain.Prizebond;

namespace PrizeBondChecker.Services
{
    public interface IPrizebondService
    {
        Task<List<Prizebond>> GetAllAsync();
        Task<PrizebondCreateModel> AddBondToList(PrizebondCreateModel prizebond);
    }
}
