using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MagicCarpetWebApp.Models;
using MagicCarpetWebApp.Services;

namespace MagicCarpetWebApp.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly MagicCarpetWebAppContext _context;
        private readonly INsService _nsService;
        private readonly CalculateController _calculateController;

        public ReservationsController(MagicCarpetWebAppContext context, INsService nsService, CalculateController calculateController)
        {
            _context = context;
            _nsService = nsService;
            _calculateController = calculateController;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var magicCarpetWebAppContext = _context.Reservations.Include(r => r.Concert);
            return View(await magicCarpetWebAppContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Concert)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            //Trying to demonstrate the calculation method
            ViewData["StillNeededSeats"] = _calculateController.GetStillNeededSeats(reservation.ConcertId, reservation.Destination);
            ViewData["Price"] = _calculateController.GetPrice(reservation.ConcertId, reservation.Destination, reservation.Amount);

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["ConcertId"] = new SelectList(_context.ConcertInfoes, "Id", "Name");
            ViewData["Stations"] = new SelectList(_nsService.GetStations().payload.Select(s => new { s.code, s.namen.lang }), "code", "lang");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConcertId,Destination,Amount,EmailAddress,Agrees,PaymentDetails")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.Id = Guid.NewGuid();
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConcertId"] = new SelectList(_context.ConcertInfoes, "Id", "Name", reservation.ConcertId);
            ViewData["Stations"] = new SelectList(_nsService.GetStations().payload.Select(s => new { s.code, s.namen.lang }), "code", "lang", reservation.Destination);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["ConcertId"] = new SelectList(_context.ConcertInfoes, "Id", "Name", reservation.ConcertId);
            ViewData["Stations"] = new SelectList(_nsService.GetStations().payload.Select(s => new { s.code, s.namen.lang }), "code", "lang", reservation.Destination);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ConcertId,Destination,Amount,EmailAddress,Agrees,PaymentDetails")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["ConcertId"] = new SelectList(_context.ConcertInfoes, "Id", "Name", reservation.ConcertId);
            ViewData["Stations"] = new SelectList(_nsService.GetStations().payload.Select(s => new { s.code, s.namen.lang }), "code", "lang", reservation.Destination);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Concert)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.Id == id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(Guid id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
