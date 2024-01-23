using SimpleBookingSystem.Infrastructure;

namespace SimpleBookingSystem.Exceptions.Resource;

public class ResourceNotFoundException: NotFoundException
{
    public ResourceNotFoundException() : base("Resource not found.")
    {
        
    }
}