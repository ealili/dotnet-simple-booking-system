using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Data;
using SimpleBookingSystem.Exceptions.Booking;
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
        return await _context.Bookings
            .Include(b => b.Resource)
            .ToListAsync();
    }


    public async Task<Booking> GetByIdAsync(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null)
        {
            throw new BookingNotFoundException();
        }
        return booking;
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    public async Task<List<Booking>> GetBookingsForResourceInDateRange(int resourceId, DateTime fromDate, DateTime toDate)
    {
        return await _context.Bookings
        .Where(b => b.ResourceId == resourceId &&
                    (fromDate <= b.ToDateTime && toDate >= b.FromDateTime))
        .ToListAsync();
    }
}