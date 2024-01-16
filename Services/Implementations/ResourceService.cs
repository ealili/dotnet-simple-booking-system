using SimpleBookingSystem.Models;
using SimpleBookingSystem.Repositories.Interfaces;
using SimpleBookingSystem.Services.Interfaces;

namespace SimpleBookingSystem.Services.Implementations;

public class ResourceService: IResourceService
{
    private readonly IResourceRepository _resourceRepository;

    public ResourceService(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }
    
    public async Task<IEnumerable<Resource>> GetAllAsync()
    {
        return await _resourceRepository.GetAllAsync();
    }
}