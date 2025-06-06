using Microsoft.AspNetCore.Mvc.Rendering;

namespace Airly.ViewModels;

public class ConnectionSearchVM
{
    public SelectList DepartureAirports { get; set; } = null!;
    public IEnumerable<TicketInfo>      Tickets          { get; set; } = Enumerable.Empty<TicketInfo>();
}

public class TicketInfo
{
    public int    TicketId     { get; set; }
    public string Passenger    { get; set; } = string.Empty;
    public string FromAirport  { get; set; } = string.Empty;
    public string ToAirport    { get; set; } = string.Empty;
}