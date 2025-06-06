using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Airly.Data;
using Airly.Models;

namespace Airly.Controllers
{
    public class AdminAirportController : Controller
    {
        private readonly AirlyContext _context;

        public AdminAirportController(AirlyContext context)
        {
            _context = context;
        }

        // GET: Airport
        [AdminOnly]
        public async Task<IActionResult> Index()
        {
            var airlyContext = _context.Airports.Include(a => a.Location);
            return View(await airlyContext.ToListAsync());
        }

        // GET: Airport/Details/5
        [AdminOnly]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // GET: Airport/Create
        [AdminOnly]
        public IActionResult Create()
        {
            var locations = _context.Locations
                .Select(l => new
                {
                    l.Id,
                    Text = $"({l.Id}) {l.City} – {l.Country}"
                })
                .ToList();

            ViewData["LocationId"] = new SelectList(locations, "Id", "Text");
            return View();
        }

        // POST: Airport/Create
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("LocationId,Name,WebsiteUrl,MapUrl")] Airport airport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // ↓ odbudowujemy drop-down dokładnie tak samo jak w GET
            ViewData["LocationId"] = new SelectList(
                _context.Locations
                    .Select(l => new
                    {
                        l.Id,
                        Text = $"{l.City} – {l.Country}"
                    })
                    .ToList(),
                "Id",
                "Text",
                airport.LocationId);      // zaznacz wybraną pozycję

            return View(airport);
        }

        // GET: Airport/Edit/5
        [AdminOnly]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", airport.LocationId);
            return View(airport);
        }

        // POST: Airport/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LocationId,Name,WebsiteUrl,MapUrl")] Airport airport)
        {
            if (id != airport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirportExists(airport.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", airport.LocationId);
            return View(airport);
        }

        // GET: Airport/Delete/5
        [AdminOnly]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // POST: Airport/Delete/5
        [AdminOnly]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airport = await _context.Airports.FindAsync(id);
            if (airport != null)
            {
                _context.Airports.Remove(airport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirportExists(int id)
        {
            return _context.Airports.Any(e => e.Id == id);
        }
    }
}
