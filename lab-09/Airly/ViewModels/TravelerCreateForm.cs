using System.ComponentModel.DataAnnotations;

namespace Airly.ViewModels;

public class TravelerCreateForm
{
    [Required, MaxLength(50)]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = string.Empty;
}