using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBookingSystem.Models;

public class Booking
{
    [Key] public int Id { get; set; }
    
    public DateTime FromDateTime { get; set; }
    public DateTime ToDateTime { get; set; }
    public int BookedQuantity { get; set; }
    
    public int ResourceId { get; set; }

    [ForeignKey("ResourceId")]
    public Resource Resource { get; set; }
}