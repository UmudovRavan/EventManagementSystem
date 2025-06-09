using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Interfaces;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class GenericService<TModel, TEntity> : IGenericService<TModel, TEntity>
    where TEntity : BaseEntity, new()
    where TModel : class, new()
    {
        private readonly IGenericRepository<TEntity> _genericRepository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<TModel> CreateAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            var result = await _genericRepository.AddAsync(entity);
            await _genericRepository.SaveChangesAsync();
            return _mapper.Map<TModel>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var success = await _genericRepository.DeleteAsync(id);
            if (success)
                await _genericRepository.SaveChangesAsync();
            return success;
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await _genericRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        public async Task<TModel> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            return _mapper.Map<TModel>(entity);
        }

        public async Task<TModel> Update(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            var updatedEntity = await _genericRepository.UpdateAsync(entity);
            await _genericRepository.SaveChangesAsync();
            return _mapper.Map<TModel>(updatedEntity);
        }
    }
}
