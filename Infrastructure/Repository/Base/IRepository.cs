using Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Base
{
    public interface IRepository<T> where T : IBaseEntity
    {
        IMongoCollection<T> Collection { get; }
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        IEnumerable<T> FilterBy(
        Expression<Func<T, bool>> filterExpression);
        Task<List<T>> FilterByAsync(
          Expression<Func<T, bool>> filterExpression);
        T FindById(Guid id);
        Task<T> FindByIdAsync(Guid id);
        T FindOne(Expression<Func<T, bool>> filterExpression);
        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);
        Task<TProjected> FindOneAsync<TProjected>(Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TProjected>> projectionExpression);
        void InsertOne(T document);
        Task InsertOneAsync(T document);
        void InsertMany(ICollection<T> documents);
        Task InsertManyAsync(ICollection<T> documents);
        //void ReplaceOne(T document);
        //Task ReplaceOneAsync(T document);
        void DeleteOne(T document);
        Task DeleteOneAsync(T document);
        void DeleteById(Guid id);
        Task DeleteByIdAsync(Guid id);
        void DeleteMany(ICollection<T> docunents);
        Task DeleteManyAsync(ICollection<T> docunents);
    }
}
