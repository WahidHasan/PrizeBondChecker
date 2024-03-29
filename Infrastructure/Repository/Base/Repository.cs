﻿using Domain;
using Infrastructure.DBContext;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : IBaseEntity
    {
        protected readonly IDbContext _mongoContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected IMongoCollection<T> _collection;
        public IMongoCollection<T> Collection => _collection;
        private Guid createdUserId;

        public Repository(IDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mongoContext = context;
            _httpContextAccessor = httpContextAccessor;
            _collection = _mongoContext.GetCollection<T>();
            var userId = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "jti").Select(x => x.Value).FirstOrDefault();
            createdUserId = userId == null ? new Guid() : new Guid(userId);
        }
        public T FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> FindByIdAsync(Guid id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual IEnumerable<T> FilterBy(
      Expression<Func<T, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual async Task<List<T>> FilterByAsync(
          Expression<Func<T, bool>> filterExpression)
        {
            return await _collection.Find(filterExpression).ToListAsync();
        }

        public virtual T FindOne(Expression<Func<T, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }
        public virtual Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual Task<TProjected> FindOneAsync<TProjected>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TProjected>> projectionExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression)
            .Project(projectionExpression).FirstOrDefaultAsync());
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.AsQueryable().ToEnumerable();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }

        public void InsertOne(T document)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertOneAsync(T document)
        {
            document.CreatedBy = createdUserId;
            document.CreatedDate = DateTime.UtcNow;
            await Task.Run(() => _collection.InsertOneAsync(document));
        }

        public void InsertMany(ICollection<T> documents)
        {
            documents.First().CreatedBy = createdUserId;
            documents.First().CreatedDate = DateTime.UtcNow;
            _collection.InsertMany(documents);
        }


        public virtual async Task InsertManyAsync(ICollection<T> documents)
        {
            documents.First().CreatedBy = createdUserId;
            documents.First().CreatedDate = DateTime.UtcNow;
            await _collection.InsertManyAsync(documents);
        }

        public void DeleteOne(Expression<Func<T, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }
        public void DeleteOne(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndDelete(filter);
        }
        public async Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            await Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }
        public async Task DeleteOneAsync(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            await Task.Run(() => _collection.FindOneAndDeleteAsync(filter));
        }

        public void DeleteMany(Expression<Func<T, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public void DeleteById(Guid id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(Guid id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public void DeleteMany(ICollection<T> documents)
        {
            var documentIds = documents.Select(a => a.Id).ToList();

            var filter = Builders<T>.Filter.In("Id", documentIds);

            _collection.DeleteMany(filter);
        }

        public Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }

        public async Task DeleteManyAsync(ICollection<T> documents)
        {
            var documentIds = documents.Select(a => a.Id).ToList();

            var filter = Builders<T>.Filter.In("Id", documentIds);

            await Task.Run(() => _collection.DeleteManyAsync(filter));
        }
    }
}
