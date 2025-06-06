namespace Airly.Models;

public class Ticket
{
    public int TravelerId   { get; set; }
    public int ConnectionId { get; set; }

    public Traveler? Traveler  { get; set; }
    public Connection? Connection { get; set; }
}