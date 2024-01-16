using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystem.Services.Interfaces;

namespace SimpleBookingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController: ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var bookings = await _bookingService.GetAllAsync();

        return Ok(bookings);
    }
}