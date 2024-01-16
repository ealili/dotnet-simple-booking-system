namespace SimpleBookingSystem.Repositories;

public interface IRepository<TEntity>
{
    public Task SaveChangesAsync();
    
    public Task<IEnumerable<TEntity>> GetAllAsync();
    
    public Task<TEntity> GetByIdAsync(int id);
    
    public Task AddAsync(TEntity entity);
}