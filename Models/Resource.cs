using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SimpleBookingSystem.Models;

public class Resource
{
    [Key] public int Id { get; set; }
    
    public required string Name { get; set; }
    public required int Quantity { get; set; }
    
    [JsonIgnore]
    public IEnumerable<Booking>? Bookings { get; set; }
}