using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SimpleBookingSystem.Models;

public class Resource
{
    [Key] public int Id { get; set; }
    
    public string Name { get; set; }
    public int Quantity { get; set; }
    
    [JsonIgnore]
    public IEnumerable<Booking> Bookings { get; set; }
}