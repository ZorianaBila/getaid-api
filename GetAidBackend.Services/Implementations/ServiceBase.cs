using AutoMapper;
using GetAidBackend.Domain;
using GetAidBackend.Services.Abstractionas;
using GetAidBackend.Services.Dtos;
using GetAidBackend.Services.Exceptions;
using GetAidBackend.Storage.Abstractions;

namespace GetAidBackend.Services.Implementations
{
    public class ServiceBase<TEntity, TDto, TRepository> : IServiceBase<TEntity, TDto>
        where TRepository : IRepository<TEntity>
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        protected readonly TRepository _repository;
        protected readonly IMapper _mapper;

        public ServiceBase(TRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TDto> GetById(string id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
                throw new NotFoundException($"The {entity.GetType().Name} {id}");

            return GetDtoFromEntity(entity);
        }

        public virtual async Task<TDto> Add(TEntity entity)
        {
            var updated = await _repository.Add(entity);
            return GetDtoFromEntity(updated);
        }

        public virtual async Task<List<TDto>> GetAll()
        {
            var entities = await _repository.GetAll();
            return entities.ConvertAll(_ => _mapper.Map<TEntity, TDto>(_));
        }

        public virtual async Task DeleteById(string id)
        {
            _ = await _repository.DeleteById(id);
        }

        public virtual async Task<TDto> Update(TEntity entity)
        {
            var updated = await _repository.Update(entity);
            return GetDtoFromEntity(updated);
        }

        protected virtual TDto GetDtoFromEntity(TEntity entity)
        {
            return _mapper.Map<TEntity, TDto>(entity);
        }

        protected virtual List<TDto> GetDtosFromEntities(List<TEntity> entities)
        {
            return entities.ConvertAll(
                _ => GetDtoFromEntity(_)
                );
        }
    }
}