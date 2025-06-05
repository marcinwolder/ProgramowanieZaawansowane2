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
    public class AdminTicketController : Controller
    {
        private readonly AirlyContext _context;

        public AdminTicketController(AirlyContext context)
        {
            _context = context;
        }

        // GET: Ticket
        [AdminOnly]
        public async Task<IActionResult> Index()
        {
            var airlyContext = _context.Tickets.Include(t => t.Connection).Include(t => t.Traveler);
            return View(await airlyContext.ToListAsync());
        }

        // GET: Ticket/Details/5
        [AdminOnly]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Connection)
                .Include(t => t.Traveler)
                .FirstOrDefaultAsync(m => m.TravelerId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Ticket/Create
        [AdminOnly]
        public IActionResult Create()
        {
            ViewData["ConnectionId"] = new SelectList(_context.Connections, "Id", "Id");
            ViewData["TravelerId"] = new SelectList(_context.Travelers, "Id", "Id");
            return View();
        }

        // POST: Ticket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TravelerId,ConnectionId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConnectionId"] = new SelectList(_context.Connections, "Id", "Id", ticket.ConnectionId);
            ViewData["TravelerId"] = new SelectList(_context.Travelers, "Id", "Id", ticket.TravelerId);
            return View(ticket);
        }

        // GET: Ticket/Edit/5
        [AdminOnly]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["ConnectionId"] = new SelectList(_context.Connections, "Id", "Id", ticket.ConnectionId);
            ViewData["TravelerId"] = new SelectList(_context.Travelers, "Id", "Id", ticket.TravelerId);
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TravelerId,ConnectionId")] Ticket ticket)
        {
            if (id != ticket.TravelerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TravelerId))
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
            ViewData["ConnectionId"] = new SelectList(_context.Connections, "Id", "Id", ticket.ConnectionId);
            ViewData["TravelerId"] = new SelectList(_context.Travelers, "Id", "Id", ticket.TravelerId);
            return View(ticket);
        }

        // GET: Ticket/Delete/5
        [AdminOnly]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Connection)
                .Include(t => t.Traveler)
                .FirstOrDefaultAsync(m => m.TravelerId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [AdminOnly]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TravelerId == id);
        }
    }
}
