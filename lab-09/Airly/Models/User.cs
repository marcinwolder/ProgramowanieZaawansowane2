using System.ComponentModel.DataAnnotations;

namespace Airly.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }

    public ICollection<Traveler>? Travelers { get; set; } = new List<Traveler>();
}