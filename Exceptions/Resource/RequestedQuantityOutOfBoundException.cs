namespace SimpleBookingSystem.Exceptions.Resource;

public class RequestedQuantityOutOfBoundException : Exception
{
    public RequestedQuantityOutOfBoundException() : base("The requested quantity is greater than existing quantity.")
    {

    }
}