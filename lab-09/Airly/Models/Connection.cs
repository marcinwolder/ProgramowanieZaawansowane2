using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airly.Models;

public class Connection
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Airport))]
    public int FromAirportId { get; set; }
    [ForeignKey(nameof(Airport))]
    public int ToAirportId { get; set; }
    
    public int NumberOfSlots { get; set; }
    
    public required Airport FromAirport { get; set; }
    public required Airport ToAirport   { get; set; }
    public ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
}