namespace Airly.Models;

public class Ticket
{
    public int TravelerId   { get; set; }
    public int ConnectionId { get; set; }

    public required Traveler Traveler  { get; set; }
    public required Connection Connection { get; set; }
}