using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DBContext
{
    public interface IDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name = "");
    }
}
