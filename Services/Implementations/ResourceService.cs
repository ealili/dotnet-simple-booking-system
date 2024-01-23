using SimpleBookingSystem.Exceptions.Resource;
using SimpleBookingSystem.Models;
using SimpleBookingSystem.Repositories.Interfaces;
using SimpleBookingSystem.Services.Interfaces;

namespace SimpleBookingSystem.Services.Implementations;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IBookingRepository _bookingRepository;

    public ResourceService(IResourceRepository resourceRepository, IBookingRepository bookingRepository)
    {
        _resourceRepository = resourceRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<IEnumerable<Resource>> GetAllAsync()
    {
        return await _resourceRepository.GetAllAsync();
    }


    public async Task<Resource> GetByIdAsync(int id)
    {
        return await _resourceRepository.GetByIdAsync(id);
    }

    public Task AddAsync(Resource entity)
    {
        throw new NotImplementedException();
    }

    public async Task CheckIfResourceIsAvailable(int resourceId, DateTime fromDate, DateTime toDate,
        int requestedQuantity)
    {
        var resource = await GetByIdAsync(resourceId);

        if (requestedQuantity > resource.Quantity)
        {
            throw new RequestedQuantityOutOfBoundException();
        }
        
        // Find any bookings that are in this timeframe to check how much they interfere with the resource quantity
        var conflictingBookings =
            await _bookingRepository.GetBookingsForResourceInDateRange(resourceId, fromDate, toDate);

        // If there are no conflicting bookings, return true directly
        if (conflictingBookings.Count == 0)
        {
            return;
        }

        // Calculate the available quantity for the requested period
        var availableQuantity = resource.Quantity;

        // Calculate available quantity by subtracting booked quantities during the chosen time window
        availableQuantity -= conflictingBookings.Where(b => b.FromDateTime >= fromDate && b.ToDateTime <= toDate)
            .Sum(b => b.BookedQuantity);
        
        // Check if the available quantity is sufficient for the requested booking
        // If available quantity is still less than requested quantity, throw exception again
        if (!(availableQuantity >= requestedQuantity))
        {
            throw new NotSufficientResourceQuantityException($"Not sufficient resource quantity. Available at the moment are  {availableQuantity}.");
        }
    }
}