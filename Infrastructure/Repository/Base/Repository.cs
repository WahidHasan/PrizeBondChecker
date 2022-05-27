using Domain;
using Infrastructure.DBContext;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : IBaseEntity
    {
        protected readonly IDbContext _mongoContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected IMongoCollection<T> _collection;
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

        public Task<T> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
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
    }
}
