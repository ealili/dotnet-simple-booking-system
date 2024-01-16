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

    public async Task<Booking> GetByIdAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        return booking;
    }

    public async Task AddAsync(Booking entity)
    {
        await _bookingRepository.AddAsync(entity);
        await _bookingRepository.SaveChangesAsync();
    }
}