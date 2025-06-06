using Airly.Data;
using Airly.Models;
using Airly.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Airly.Controllers;

public class TicketController : Controller
{
    private readonly AirlyContext _context;

    public TicketController(AirlyContext context)
    {
        _context = context;
    }
    
    // GET
    [SessionAuthorize]
    public async Task<IActionResult> Index()
    {
        var departureAirports = await _context.Connections
            .Select(c => new { c.FromAirport!.Id, c.FromAirport.Name })
            .Distinct()
            .OrderBy(a => a.Name)
            .ToListAsync();

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return NotFound();

        var tickets = await _context.Tickets
            .Include(t => t.Traveler)
            .Include(t => t.Connection)
            .ThenInclude(c => c!.FromAirport)
            .Include(t => t.Connection)
            .ThenInclude(c => c!.ToAirport)
            .Where(t => t.Traveler!.UserId == userId)
            .Select(t => new TicketInfo
            {
                TicketId    = t.TravelerId,          // lub t.Id, jeśli dodałeś Id w Ticket
                Passenger   = $"{t.Traveler!.FirstName} {t.Traveler.LastName}",
                FromAirport = t.Connection!.FromAirport!.Name,
                ToAirport   = t.Connection.ToAirport!.Name
            })
            .ToListAsync();

        var vm = new ConnectionSearchVM
        {
            DepartureAirports = new SelectList(departureAirports, "Id", "Name"),
            Tickets           = tickets
        };

        return View(vm);
    }
    
    // GET /Airports/GetDestinations?from=3
    [HttpGet]
    public async Task<IActionResult> GetDestinations(int from)
    {
        var destinations = await _context.Connections
            .Where(c => c.FromAirportId == from)
            .Select(c => new
            {
                id   = c.ToAirport!.Id,        // ← małe „i”
                name = c.ToAirport.Name       // ← małe „n”
            })
            .Distinct()
            .OrderBy(a => a.name)
            .ToListAsync();

        return Json(destinations);
    }

// GET /Airports/GetConnections?from=3&to=7
    [HttpGet]
    public async Task<IActionResult> GetConnections(int from, int? to)
    {
        var q = _context.Connections
            .Include(c => c.FromAirport)
            .Include(c => c.ToAirport)
            .Where(c => c.FromAirportId == from);

        if (to.HasValue)
            q = q.Where(c => c.ToAirportId == to.Value);

        var list = await q.Select(c => new
            {
                id             = c.Id,
                from           = c.FromAirport!.Name,
                to             = c.ToAirport!.Name,
                numberOfSlots  = c.NumberOfSlots
            })
            .ToListAsync();

        return Json(list);
    }
    
    // GET: /Tickets/Buy?connectionId=5
    [SessionAuthorize]
    public async Task<IActionResult> Buy(int connectionId)
    {
        var connection = await _context.Connections
                                   .Include(c => c.FromAirport)
                                   .Include(c => c.ToAirport)
                                   .FirstOrDefaultAsync(c => c.Id == connectionId);
        if (connection is null) return NotFound();

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return NotFound();

        var travelers = await _context.Travelers
                                  .Where(t => t.UserId == userId)
                                  .ToListAsync();

        var vm = new TicketPurchaseVM
        {
            Connection   = connection,
            Travelers    = travelers,
            ConnectionId = connectionId
        };
        return View(vm);                     // Views/Tickets/Buy.cshtml
    }

    // POST: /Tickets/Buy
    [SessionAuthorize]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Buy(TicketPurchaseForm form)
    {
        if (!ModelState.IsValid)
            return await Buy(form.ConnectionId);

        var connection = await _context.Connections
                                   .Include(c => c.Tickets)
                                   .FirstOrDefaultAsync(c => c.Id == form.ConnectionId);
        if (connection is null) return NotFound();

        // sprawdź dostępność miejsc
        if (connection.Tickets!.Count >= connection.NumberOfSlots)
        {
            ModelState.AddModelError(string.Empty, "No free slots for this connection.");
            return await Buy(form.ConnectionId);
        }

        _context.Tickets.Add(new Ticket
        {
            TravelerId   = form.TravelerId,
            ConnectionId = form.ConnectionId
        });
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Success), new { id = form.ConnectionId });
    }

    // GET: /Tickets/Success/5
    [SessionAuthorize]
    public async Task<IActionResult> Success(int id)
    {
        var conn = await _context.Connections
                             .Include(c => c.FromAirport)
                             .Include(c => c.ToAirport)
                             .FirstOrDefaultAsync(c => c.Id == id);
        return View(conn);
    }
}
