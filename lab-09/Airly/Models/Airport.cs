using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airly.Models;

public class Airport
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Location))]
    public int LocationId { get; set; }
    
    [Required, MaxLength(100)]
    public required string Name { get; set; }
    [Url]
    public string? WebsiteUrl { get; set; }
    [Url]
    public string? MapUrl { get; set; }
    public required Location Location { get; set; }
    public ICollection<Connection>? DepartingConnections { get; set; } = new List<Connection>();
    public ICollection<Connection>? ArrivingConnections  { get; set; } = new List<Connection>();
}