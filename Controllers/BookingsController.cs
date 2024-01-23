using FluentValidation;
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

        var response = new ApiResponse<IEnumerable<Booking>>(false, "Data retrieved successfully.", bookings);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromServices] IValidator<BookingDto> validator,
        BookingDto bookingDto)
    {
        await validator.ValidateAndThrowAsync(bookingDto);

        await _resourceService.CheckIfResourceIsAvailable(bookingDto.ResourceId,
            bookingDto.FromDateTime, bookingDto.ToDateTime,
            bookingDto.BookedQuantity);

        var booking = new Booking
        {
            FromDateTime = bookingDto.FromDateTime,
            ToDateTime = bookingDto.ToDateTime,
            BookedQuantity = bookingDto.BookedQuantity,
            ResourceId = bookingDto.ResourceId
        };

        await _bookingService.AddAsync(booking);
        var response = new ApiResponse<Booking>(true, "Booking created successfully.", booking);
        return Ok(response);
    }
}