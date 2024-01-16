namespace SimpleBookingSystem.Services;

public interface IService<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task<TEntity> GetByIdAsync(int id);
    
    Task AddAsync(TEntity entity);
}