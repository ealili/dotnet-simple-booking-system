namespace SimpleBookingSystem.Repositories;

public interface IRepository<TEntity>
{
    public Task SaveChangesAsync();
    
    public Task<IEnumerable<TEntity>> GetAllAsync();
}