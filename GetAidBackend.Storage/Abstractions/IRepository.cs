using GetAidBackend.Domain;
using System.Linq.Expressions;

namespace GetAidBackend.Storage.Abstractions
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetById(string id);

        Task<TEntity> Add(TEntity entity);

        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> DeleteById(string id);

        Task<TEntity> Update(TEntity entity);
    }
}