using GetAidBackend.Domain;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace GetAidBackend.Storage.Abstractions
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetById(string id);

        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> DeleteById(string id);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Add(TEntity entity, IClientSessionHandle session = null);
    }
}