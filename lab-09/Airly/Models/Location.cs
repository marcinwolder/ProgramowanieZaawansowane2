using System.ComponentModel.DataAnnotations;

namespace Airly.Models;

public class Location
{
    [Key]
    public int Id { get; set; }
    
    public required string Country { get; set; }
    public required string City { get; set; }
    public required string Description { get; set; }
    public string? ImgUrl { get; set; }
    
    public ICollection<Airport> Airports { get; set; } = new List<Airport>();
}