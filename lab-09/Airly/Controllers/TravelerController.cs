using Airly.Data;
using Airly.Models;
using Airly.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airly.Controllers;

[SessionAuthorize]
public class TravelerController : Controller
{
    private readonly AirlyContext _context;

    public TravelerController(AirlyContext context) => _context = context;

    // GET: /Traveler
    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return NotFound();

        var travelers = await _context.Travelers
            .Where(t => t.UserId == userId)
            .ToListAsync();

        return View(travelers);          // Views/Traveler/Index.cshtml
    }

    // GET: /Traveler/Create
    public IActionResult Create() => View();

    // POST: /Traveler/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TravelerCreateForm form)
    {
        if (!ModelState.IsValid) return View(form);

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return NotFound();

        var traveler = new Traveler
        {
            UserId    = (int)userId,
            FirstName = form.FirstName.Trim(),
            LastName  = form.LastName.Trim()
        };

        _context.Travelers.Add(traveler);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    // GET: /Traveler/Delete/5  (okno potwierdzenia)
    public async Task<IActionResult> Delete(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return NotFound();

        var traveler = await _context.Travelers
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        return traveler is null ? NotFound() : View(traveler);
    }

// POST: /Traveler/Delete/5  (faktyczne usunięcie)
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return NotFound();

        var traveler = await _context.Travelers
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (traveler is null) return RedirectToAction(nameof(Index));
        _context.Travelers.Remove(traveler);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}