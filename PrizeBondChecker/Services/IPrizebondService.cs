using PrizeBondChecker.Domain;
using PrizeBondChecker.Domain.Prizebond;

namespace PrizeBondChecker.Services
{
    public interface IPrizebondService
    {
        Task<List<PrizeBond>> GetAllAsync();
        Task<PrizebondCreateModel> AddBondToList(PrizebondCreateModel prizebond);
    }
}
