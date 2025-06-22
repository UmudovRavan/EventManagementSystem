using CodeAcademyEventManagementSystem.Entities;

namespace CodeAcademyEventManagementSystem.Repository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);

        Task SaveChangesAsync(); 
    }
}
