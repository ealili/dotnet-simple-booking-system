using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.Services.Interfaces;

public interface IResourceService : IService<Resource>
{
    Task<bool> IsResourceAvailable(int resourceId, DateTime fromDate, DateTime toDate, int requestedQuantity);
}