﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MagicCarpetWebApp.Models;

namespace MagicCarpetWebApp.Controllers
{
    public class ConcertInfoesController : Controller
    {
        private readonly MagicCarpetWebAppContext _context;

        public ConcertInfoesController(MagicCarpetWebAppContext context)
        {
            _context = context;
        }

        // GET: ConcertInfoes
        public async Task<IActionResult> Index()
        {
            var magicCarpetWebAppContext = _context.ConcertInfoes.Include(c => c.Location);
            return View(await magicCarpetWebAppContext.ToListAsync());
        }

        // GET: ConcertInfoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertInfo = await _context.ConcertInfoes
                .Include(c => c.Location)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (concertInfo == null)
            {
                return NotFound();
            }

            return View(concertInfo);
        }

        // GET: ConcertInfoes/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.ConcertLocations, "Id", "Name");
            return View();
        }

        // POST: ConcertInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,LocationId")] ConcertInfo concertInfo)
        {
            if (ModelState.IsValid)
            {
                concertInfo.Id = Guid.NewGuid();
                _context.Add(concertInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.ConcertLocations, "Id", "Name", concertInfo.LocationId);
            return View(concertInfo);
        }

        // GET: ConcertInfoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertInfo = await _context.ConcertInfoes.SingleOrDefaultAsync(m => m.Id == id);
            if (concertInfo == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.ConcertLocations, "Id", "Name", concertInfo.LocationId);
            return View(concertInfo);
        }

        // POST: ConcertInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Date,LocationId")] ConcertInfo concertInfo)
        {
            if (id != concertInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concertInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertInfoExists(concertInfo.Id))
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
            ViewData["LocationId"] = new SelectList(_context.ConcertLocations, "Id", "Name", concertInfo.LocationId);
            return View(concertInfo);
        }

        // GET: ConcertInfoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertInfo = await _context.ConcertInfoes
                .Include(c => c.Location)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (concertInfo == null)
            {
                return NotFound();
            }

            return View(concertInfo);
        }

        // POST: ConcertInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var concertInfo = await _context.ConcertInfoes.SingleOrDefaultAsync(m => m.Id == id);
            _context.ConcertInfoes.Remove(concertInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertInfoExists(Guid id)
        {
            return _context.ConcertInfoes.Any(e => e.Id == id);
        }
    }
}
