using Airly.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airly.Controllers;

public class DestinationController : Controller
{
    private readonly AirlyContext _context;

    public DestinationController(AirlyContext context)
    {
        _context = context;
    }
    
    [SessionAuthorize]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Locations.ToListAsync());
    }
}