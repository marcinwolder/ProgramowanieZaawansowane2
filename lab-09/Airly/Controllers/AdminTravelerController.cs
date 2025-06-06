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
    public class AdminTravelerController : Controller
    {
        private readonly AirlyContext _context;

        public AdminTravelerController(AirlyContext context)
        {
            _context = context;
        }

        // GET: Traveler
        [AdminOnly]
        public async Task<IActionResult> Index()
        {
            var airlyContext = _context.Travelers.Include(t => t.User);
            return View(await airlyContext.ToListAsync());
        }

        // GET: Traveler/Details/5
        [AdminOnly]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveler = await _context.Travelers
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traveler == null)
            {
                return NotFound();
            }

            return View(traveler);
        }

        // GET: Traveler/Create
        [AdminOnly]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Traveler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FirstName,LastName")] Traveler traveler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(traveler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", traveler.UserId);
            return View(traveler);
        }

        // GET: Traveler/Edit/5
        [AdminOnly]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveler = await _context.Travelers.FindAsync(id);
            if (traveler == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", traveler.UserId);
            return View(traveler);
        }

        // POST: Traveler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,FirstName,LastName")] Traveler traveler)
        {
            if (id != traveler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(traveler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelerExists(traveler.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", traveler.UserId);
            return View(traveler);
        }

        // GET: Traveler/Delete/5
        [AdminOnly]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveler = await _context.Travelers
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traveler == null)
            {
                return NotFound();
            }

            return View(traveler);
        }

        // POST: Traveler/Delete/5
        [AdminOnly]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var traveler = await _context.Travelers.FindAsync(id);
            if (traveler != null)
            {
                _context.Travelers.Remove(traveler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelerExists(int id)
        {
            return _context.Travelers.Any(e => e.Id == id);
        }
    }
}
