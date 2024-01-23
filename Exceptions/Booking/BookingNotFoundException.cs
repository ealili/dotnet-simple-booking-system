using SimpleBookingSystem.Infrastructure;

namespace SimpleBookingSystem.Exceptions.Booking;

public class BookingNotFoundException: NotFoundException
{
    public BookingNotFoundException() : base("Booking not found.")
    {
        
    }
}