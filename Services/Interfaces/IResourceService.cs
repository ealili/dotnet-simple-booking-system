using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.Services.Interfaces;

public interface IResourceService : IService<Resource>
{
    Task CheckIfResourceIsAvailable(int resourceId, DateTime fromDate, DateTime toDate, int requestedQuantity);
}