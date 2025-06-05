using System.ComponentModel.DataAnnotations;

namespace Airly.Models;

public class Ticket
{
    [Key]
    public int Id { get; set; }
    required public int Quantity { get; set; }
    public ICollection<User> ?Users { get; set; }
    public int DestinationId { get; set; }
    required public Destination Destination { get; set; } = null!;
}
