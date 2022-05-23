using AutoMapper;
using MongoDB.Driver;
using PrizeBondChecker.Domain;
using PrizeBondChecker.Domain.Prizebond;
using PrizeBondChecker.Models;

namespace PrizeBondChecker.Services
{
    public class PrizebondService : IPrizebondService
    {
        private readonly IMongoCollection<PrizeBond> _prizebond;
        private readonly IMapper _mapper;

        public PrizebondService(IPrizebondDbSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _prizebond = database.GetCollection<PrizeBond>(settings.PrizebondCheckerCollectionName);
            _mapper = mapper;
        }

        public async Task<PrizebondCreateModel> AddBondToList(PrizebondCreateModel prizebond)
        {
            var mappedData = _mapper.Map<PrizeBond>(prizebond);
            await _prizebond.InsertOneAsync(mappedData);
            return prizebond;
        }

        public async Task<List<PrizeBond>> GetAllAsync()
        {
            return await _prizebond.Find(s => true).ToListAsync();
        }
    }
}
