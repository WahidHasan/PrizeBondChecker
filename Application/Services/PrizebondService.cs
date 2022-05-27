using AutoMapper;
using Infrastructure.Repository.Base;
using MongoDB.Driver;
using PrizeBondChecker.Domain.Prizebond;

namespace PrizeBondChecker.Services
{
    public class PrizebondService : IPrizebondService
    {
        private readonly IMongoCollection<Prizebond> _prizebond;
        private readonly IRepository<Prizebond> _prizebondRepository;
        private readonly IMapper _mapper;

        public PrizebondService(IRepository<Prizebond> prizebondRepository, IMapper mapper)
        {
            //var client = new MongoClient(settings.ConnectionString);
            //var database = client.GetDatabase(settings.DatabaseName);
            //_prizebond = database.GetCollection<PrizeBond>(settings.PrizebondCheckerCollectionName);
            _prizebondRepository = prizebondRepository;
            _mapper = mapper;
        }

        public async Task<PrizebondCreateModel> AddBondToList(PrizebondCreateModel prizebond)
        {
            var mappedData = _mapper.Map<Prizebond>(prizebond);
            await _prizebondRepository.InsertOneAsync(mappedData);
            //await _prizebond.InsertOneAsync(mappedData);
            return null;
        }

        public async Task<List<Prizebond>> GetAllAsync()
        {

            return await _prizebondRepository.GetAllAsync();
        }
    }
}
