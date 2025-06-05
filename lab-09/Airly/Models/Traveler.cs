using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airly.Models;

public class Traveler
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Models.User))]
    public int UserId { get; set; }
    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    
    public required User User { get; set; }
    public ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
}