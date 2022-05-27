using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DBContext
{
    internal class DbContext : IDbContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public DbContext(IOptions<DbConnectionDetails> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.ConnectionString);
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }
        public IMongoCollection<T> GetCollection<T>(string name = "")
        {
            return _db.GetCollection<T>(GetCollectionName<T>(name));
        }

        private string GetCollectionName<TCollection>(string collectionName = "") =>
    string.IsNullOrEmpty(collectionName) ? $"{typeof(TCollection).Name}" : collectionName;
    }
}
