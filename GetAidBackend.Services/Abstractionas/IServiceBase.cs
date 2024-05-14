using GetAidBackend.Domain;
using GetAidBackend.Services.Dtos;

namespace GetAidBackend.Services.Abstractionas
{
    public interface IServiceBase<TEntity, TDto>
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        Task<TDto> Add(TEntity entity);

        Task DeleteById(string id);

        Task<List<TDto>> GetAll();

        Task<TDto> GetById(string id);

        Task<TDto> Update(TEntity entity);
    }
}