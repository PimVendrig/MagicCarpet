using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MagicCarpetWebApp.Models;

namespace MagicCarpetWebApp.Controllers
{
    public class ConcertLocationsController : Controller
    {
        private readonly MagicCarpetWebAppContext _context;

        public ConcertLocationsController(MagicCarpetWebAppContext context)
        {
            _context = context;
        }

        // GET: ConcertLocations
        public async Task<IActionResult> Index()
        {
            return View(await _context.ConcertLocations.ToListAsync());
        }

        // GET: ConcertLocations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertLocation = await _context.ConcertLocations
                .SingleOrDefaultAsync(m => m.Id == id);
            if (concertLocation == null)
            {
                return NotFound();
            }

            return View(concertLocation);
        }

        // GET: ConcertLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConcertLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Station")] ConcertLocation concertLocation)
        {
            if (ModelState.IsValid)
            {
                concertLocation.Id = Guid.NewGuid();
                _context.Add(concertLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concertLocation);
        }

        // GET: ConcertLocations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertLocation = await _context.ConcertLocations.SingleOrDefaultAsync(m => m.Id == id);
            if (concertLocation == null)
            {
                return NotFound();
            }
            return View(concertLocation);
        }

        // POST: ConcertLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Station")] ConcertLocation concertLocation)
        {
            if (id != concertLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concertLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertLocationExists(concertLocation.Id))
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
            return View(concertLocation);
        }

        // GET: ConcertLocations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertLocation = await _context.ConcertLocations
                .SingleOrDefaultAsync(m => m.Id == id);
            if (concertLocation == null)
            {
                return NotFound();
            }

            return View(concertLocation);
        }

        // POST: ConcertLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var concertLocation = await _context.ConcertLocations.SingleOrDefaultAsync(m => m.Id == id);
            _context.ConcertLocations.Remove(concertLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertLocationExists(Guid id)
        {
            return _context.ConcertLocations.Any(e => e.Id == id);
        }
    }
}
