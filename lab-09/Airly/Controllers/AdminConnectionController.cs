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
    public class AdminConnectionController : Controller
    {
        private readonly AirlyContext _context;

        public AdminConnectionController(AirlyContext context)
        {
            _context = context;
        }

        // GET: Connection
        [AdminOnly]
        public async Task<IActionResult> Index()
        {
            var airlyContext = _context.Connections.Include(c => c.FromAirport).Include(c => c.ToAirport);
            return View(await airlyContext.ToListAsync());
        }

        // GET: Connection/Details/5
        [AdminOnly]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connection = await _context.Connections
                .Include(c => c.FromAirport)
                .Include(c => c.ToAirport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (connection == null)
            {
                return NotFound();
            }

            return View(connection);
        }

        // GET: Connection/Create
        [AdminOnly]
        public IActionResult Create()
        {
            ViewData["FromAirportId"] = new SelectList(_context.Airports, "Id", "Name");
            ViewData["ToAirportId"] = new SelectList(_context.Airports, "Id", "Name");
            return View();
        }

        // POST: Connection/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FromAirportId,ToAirportId,NumberOfSlots")] Connection connection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(connection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromAirportId"] = new SelectList(_context.Airports, "Id", "Name", connection.FromAirportId);
            ViewData["ToAirportId"] = new SelectList(_context.Airports, "Id", "Name", connection.ToAirportId);
            return View(connection);
        }

        // GET: Connection/Edit/5
        [AdminOnly]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connection = await _context.Connections.FindAsync(id);
            if (connection == null)
            {
                return NotFound();
            }
            ViewData["FromAirportId"] = new SelectList(_context.Airports, "Id", "Name", connection.FromAirportId);
            ViewData["ToAirportId"] = new SelectList(_context.Airports, "Id", "Name", connection.ToAirportId);
            return View(connection);
        }

        // POST: Connection/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FromAirportId,ToAirportId,NumberOfSlots")] Connection connection)
        {
            if (id != connection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(connection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConnectionExists(connection.Id))
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
            ViewData["FromAirportId"] = new SelectList(_context.Airports, "Id", "Name", connection.FromAirportId);
            ViewData["ToAirportId"] = new SelectList(_context.Airports, "Id", "Name", connection.ToAirportId);
            return View(connection);
        }

        // GET: Connection/Delete/5
        [AdminOnly]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connection = await _context.Connections
                .Include(c => c.FromAirport)
                .Include(c => c.ToAirport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (connection == null)
            {
                return NotFound();
            }

            return View(connection);
        }

        // POST: Connection/Delete/5
        [AdminOnly]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var connection = await _context.Connections.FindAsync(id);
            if (connection != null)
            {
                _context.Connections.Remove(connection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConnectionExists(int id)
        {
            return _context.Connections.Any(e => e.Id == id);
        }
    }
}
