using AutoMapper;
using Infrastructure.Repository.Base;
using PrizeBondChecker.Domain;
using PrizeBondChecker.Domain.Prizebond;

namespace PrizeBondChecker.Services
{
    public class PrizebondService : IPrizebondService
    {
        private readonly IRepository<Prizebond> _prizebondRepository;
        private readonly IRepository<Users> _usersRepository;
        private readonly IMapper _mapper;

        public PrizebondService(IRepository<Prizebond> prizebondRepository, IRepository<Users> usersRepository, IMapper mapper)
        {
            _prizebondRepository = prizebondRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<PrizebondCreateModel> AddBondToList(PrizebondCreateModel prizebond)
        {
            if (prizebond?.UserId != null)
            {
                await _usersRepository.FindByIdAsync(prizebond.UserId);
                var mappedData = _mapper.Map<Prizebond>(prizebond);
                await _prizebondRepository.InsertOneAsync(mappedData);
                return prizebond;
            }
            return default;
        }

        public async Task<List<Prizebond>> GetAllAsync()
        {

            return await _prizebondRepository.GetAllAsync();
        }
    }
}
