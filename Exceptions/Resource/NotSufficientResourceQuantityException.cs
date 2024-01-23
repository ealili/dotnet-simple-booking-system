using SimpleBookingSystem.Infrastructure;

namespace SimpleBookingSystem.Exceptions.Resource;

public class NotSufficientResourceQuantityException: BadRequestException
{
    public NotSufficientResourceQuantityException() : base("Not sufficient resource quantity.")
    {

    }
    
    public NotSufficientResourceQuantityException(string message) : base(message)
    {
    }
}