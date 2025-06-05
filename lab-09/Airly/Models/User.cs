using System.ComponentModel.DataAnnotations;

namespace Airly.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    required public string Email { get; set; }
    required public string Password { get; set; }
    public ICollection<Ticket> ?Tickets { get; set; }
}