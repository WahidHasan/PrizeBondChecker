using MongoDB.Driver;
using PrizeBondChecker.Domain;
using PrizeBondChecker.Models;
using System.Diagnostics;
using static PrizeBondChecker.Domain.Enums.EnumPrizebond;

namespace PrizeBondChecker.Services
{
    public class SchedulerService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMongoCollection<PrizeBond> _prizebond;
        public SchedulerService(IPrizebondDbSettings settings)
        {
            //_serviceScopeFactory = serviceScopeFactory;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _prizebond = database.GetCollection<PrizeBond>(settings.PrizebondCheckerCollectionName);
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Debug.WriteLine("Hello Scheduler");
                TestFunction(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
              

            }
        }

        private async void TestFunction(CancellationToken stoppingToken)
        {
            List <PrizeBond> updatedList= new List<PrizeBond>();
            foreach (var bond in _prizebond.Find(item => item.Checked != CheckBond.Checked &&  item.entryDate < DateTime.Now).ToList())
            {
                Debug.WriteLine(bond.bondId);
                bond.Checked = CheckBond.Checked;
                Debug.WriteLine(bond.Checked);
                updatedList.Add(bond);
            }
            //var filterDefinition = Builders<PrizeBond>.Filter.Eq(p => p.Checked, CheckBond.Unchecked);
            var filterDefinition = Builders<PrizeBond>.Filter.Lt(p => p.entryDate, DateTime.Now);
            var updateDefinition = Builders<PrizeBond>.Update.Set(p => p.Checked, CheckBond.Checked);
            //await _prizebond.BulkWriteAsync(filterDefinition, updateDefinition);
            //Debug.WriteLine(" tasks runs in every 5 seconds");
            await _prizebond.UpdateManyAsync(filterDefinition, updateDefinition);
        }
    }
}
//item.Checked != CheckBond.Checked &&