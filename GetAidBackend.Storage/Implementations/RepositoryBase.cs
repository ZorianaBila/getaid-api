using GetAidBackend.Domain;
using GetAidBackend.Storage.Abstractions;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace GetAidBackend.Storage.Implementations
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected IMongoCollection<TEntity> _collection;

        public RepositoryBase(IMongoClient client, string collectionName)
        {
            var database = client.GetDatabase(Environment
                           .GetEnvironmentVariable("MONGO_DATABASE_NAME"));

            _collection = database.GetCollection<TEntity>(collectionName);
        }

        public async Task<TEntity> GetById(string id)
        {
            return await _collection.Find(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _collection.Find(predicate ?? Builders<TEntity>.Filter.Empty).ToListAsync();
        }

        public async Task<TEntity> DeleteById(string id)
        {
            return await _collection.FindOneAndDeleteAsync(_ => _.Id == id);
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            var options = new FindOneAndReplaceOptions<TEntity>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await _collection.FindOneAndReplaceAsync<TEntity>(_ =>
                _.Id == entity.Id, entity, options);
        }
    }
}