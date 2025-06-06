using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Airly.Models;
using Microsoft.EntityFrameworkCore;
using Airly.Data;
using System.Threading.Tasks;

namespace Airly.Controllers;

public class HomeController : Controller
{
    private readonly AirlyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, AirlyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["TicketCount"] = _context.Tickets.Count();
        ViewBag.MostCommonDestination = _context.Tickets
            .GroupBy(t => t.Connection!.ToAirport!.Location!.City)
            .Select(group => new
            {
                City = group.Key,
                Count = group.Count(),
                Location = group.First().Connection!.ToAirport!.Location
            })
            .OrderByDescending(x => x.Count)
            .First();
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
