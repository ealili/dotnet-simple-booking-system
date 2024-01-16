using SimpleBookingSystem.Models;
using SimpleBookingSystem.Repositories.Interfaces;
using SimpleBookingSystem.Services.Interfaces;

namespace SimpleBookingSystem.Services.Implementations;

public class BookingService: IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _bookingRepository.GetAllAsync(); 
    }
}