using System.ComponentModel.DataAnnotations;

namespace SimpleBookingSystem.Models;

public class Resource
{
    [Key] public int Id { get; set; }
    
    public string Name { get; set; }
    public int Quantity { get; set; }
    
    public ICollection<Booking> Bookings { get; set; }
}