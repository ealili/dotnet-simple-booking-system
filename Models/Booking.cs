using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBookingSystem.Models;

public class Booking
{
    [Key] public int Id { get; set; }
    
    public required DateTime FromDateTime { get; set; }
    public required DateTime ToDateTime { get; set; }
    public required int BookedQuantity { get; set; }
    
    public required int ResourceId { get; set; }

    [ForeignKey("ResourceId")]
    public Resource? Resource { get; set; }
}