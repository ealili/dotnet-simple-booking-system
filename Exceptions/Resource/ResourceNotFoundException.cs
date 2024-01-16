namespace SimpleBookingSystem.Exceptions.Resource;

public class ResourceNotFoundException: Exception
{
    public ResourceNotFoundException() : base("Resource not found.")
    {
        
    }
}