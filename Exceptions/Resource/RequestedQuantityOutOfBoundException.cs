using SimpleBookingSystem.Infrastructure;

namespace SimpleBookingSystem.Exceptions.Resource;

public class RequestedQuantityOutOfBoundException : BadRequestException
{
    public RequestedQuantityOutOfBoundException() : base("The requested quantity is greater than existing quantity.")
    {

    }
}