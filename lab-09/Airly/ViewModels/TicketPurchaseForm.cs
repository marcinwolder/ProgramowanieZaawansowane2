using System.ComponentModel.DataAnnotations;

namespace Airly.ViewModels;

public class TicketPurchaseForm
{
    [Required] public int ConnectionId { get; set; }
    [Required] public int TravelerId   { get; set; }
}
