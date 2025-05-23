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
    public class DestinationController : Controller
    {
        private readonly AirlyContext _context;

        public DestinationController(AirlyContext context)
        {
            _context = context;
        }

        // GET: Destination
        [SessionAuthorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Destination.ToListAsync());
        }

        // GET: Destination/Details/5
        // [SessionAuthorize]
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var destination = await _context.Destination
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (destination == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(destination);
        // }

        // GET: Destination/Create
        [SessionAuthorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Destination/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize]
        public async Task<IActionResult> Create([Bind("Id,City,ImgUrl,Description,Price")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(destination);
        }

        // GET: Destination/Edit/5
        // [SessionAuthorize]
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var destination = await _context.Destination.FindAsync(id);
        //     if (destination == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(destination);
        // }

        // POST: Destination/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // [SessionAuthorize]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,City,ImgUrl,Description,Price")] Destination destination)
        // {
        //     if (id != destination.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(destination);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!DestinationExists(destination.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(destination);
        // }

        // GET: Destination/Delete/5
        // [SessionAuthorize]
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var destination = await _context.Destination
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (destination == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(destination);
        // }

        // // POST: Destination/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // [SessionAuthorize]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var destination = await _context.Destination.FindAsync(id);
        //     if (destination != null)
        //     {
        //         _context.Destination.Remove(destination);
        //     }

        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        [SessionAuthorize]
        private bool DestinationExists(int id)
        {
            return _context.Destination.Any(e => e.Id == id);
        }
    }
}
