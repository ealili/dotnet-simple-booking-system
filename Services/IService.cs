namespace SimpleBookingSystem.Services;

public interface IService<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
}