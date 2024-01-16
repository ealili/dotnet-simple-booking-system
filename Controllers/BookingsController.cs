using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystem.DTOs;
using SimpleBookingSystem.Models;
using SimpleBookingSystem.Services.Interfaces;
using SimpleBookingSystem.Utilities.Helpers;

namespace SimpleBookingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IResourceService _resourceService;

    public BookingsController(IBookingService bookingService, IResourceService resourceService)
    {
        _bookingService = bookingService;
        _resourceService = resourceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var bookings = await _bookingService.GetAllAsync();

        return Ok(bookings);
    }

    [HttpPost]
    public async Task< IActionResult> CreateBooking([FromBody]BookingDto bookingDto)
    {
        try
        {
            var resourceIsAvailable =  await _resourceService.IsResourceAvailable(bookingDto.ResourceId,
                bookingDto.FromDateTime, bookingDto.ToDateTime,
                bookingDto.BookedQuantity);
            
            if (!resourceIsAvailable)
            {
                var errorResponse = new ApiResponse<string>(false, "Not enough resource quantity.");
                return BadRequest(errorResponse);
            }

            var booking = new Booking
            {
                FromDateTime = bookingDto.FromDateTime,
                ToDateTime = bookingDto.ToDateTime,
                BookedQuantity = bookingDto.BookedQuantity,
                ResourceId = bookingDto.ResourceId
            };

            await _bookingService.AddAsync(booking);
            var response = new ApiResponse<Booking>(true, "Created.", booking);
            return Ok(response);
        }
        catch (Exception exception)
        {
            var errorResponse = new ApiResponse<string>(false, exception.Message);
            return BadRequest(errorResponse);
        }
    }
}