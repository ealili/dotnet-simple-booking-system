using FluentValidation;
using FluentValidation.Results;
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
    public async Task< IActionResult> CreateBooking(IValidator<BookingDto> validator, BookingDto bookingDto)
    {
        var validationResult = await validator.ValidateAsync(bookingDto);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );
            return BadRequest(errors);
        }
        
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
            var response = new ApiResponse<Booking>(true, "Booking created successfully.", booking);
            return Ok(response);
        }
        catch (Exception exception)
        {
            var errorResponse = new ApiResponse<string>(false, exception.Message);
            return BadRequest(errorResponse);
        }
    }
}