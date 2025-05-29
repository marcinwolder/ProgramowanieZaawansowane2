using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace Airly.Models;

public class Destination
{
    [Key]
    public int Id { get; set; }
    required public string City { get; set; }
    required public string ImgUrl { get; set; }
    required public string Description { get; set; }
    required public int Price { get; set; }
}