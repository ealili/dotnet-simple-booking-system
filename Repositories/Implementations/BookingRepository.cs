using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Data;
using SimpleBookingSystem.Models;
using SimpleBookingSystem.Repositories.Interfaces;

namespace SimpleBookingSystem.Repositories.Implementations;

public class BookingRepository: IBookingRepository
{
    private readonly DataContext _context;

    public BookingRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _context.Bookings.ToListAsync();
    }
}