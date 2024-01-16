using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.Repositories.Interfaces;

public interface IBookingRepository: IRepository<Booking>
{ 
    Task<List<Booking>> GetBookingsForResourceInDateRange(int resourceId, DateTime fromDate, DateTime toDate);
}