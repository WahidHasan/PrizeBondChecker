using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Base
{
    public interface IRepository<T> where T : IBaseEntity
    {
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        T FindById(Guid id);
        Task<T> FindByIdAsync(Guid id);
        void InsertOne(T document);
        Task InsertOneAsync(T document);
        //void InsertMany(ICollection<T> documents);
        //Task InsertManyAsync(ICollection<T> documents);
        //void ReplaceOne(T document);
        //Task ReplaceOneAsync(T document);
        //void DeleteOne(T document);
        //Task DeleteOneAsync(T document);
        //void DeleteById(Guid id);
        //Task DeleteByIdAsync(Guid id);
        //void DeleteMany(ICollection<T> docunents);
        //Task DeleteManyAsync(ICollection<T> docunents);
    }
}
