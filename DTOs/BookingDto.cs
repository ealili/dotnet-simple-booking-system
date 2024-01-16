using System.ComponentModel.DataAnnotations;

namespace SimpleBookingSystem.DTOs;

public class BookingDto
{
    public DateTime FromDateTime { get; set; }
        
    public DateTime ToDateTime { get; set; }
        
    [Range(1, int.MaxValue, ErrorMessage = "Booked quantity must be greater than 0")]
    public int BookedQuantity { get; set; }
        
    public int ResourceId { get; set; }
}