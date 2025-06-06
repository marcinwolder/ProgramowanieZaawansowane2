using Airly.Models;

namespace Airly.ViewModels;

public class TicketPurchaseVM
{
    public Connection Connection { get; set; } = null!;
    public IEnumerable<Traveler> Travelers { get; set; } = new List<Traveler>();

    public int ConnectionId { get; set; }
    public int? TravelerId  { get; set; }
}
