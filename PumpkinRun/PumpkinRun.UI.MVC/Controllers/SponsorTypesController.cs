using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PumpkinRun.DATA.EF.Models;

namespace PumpkinRun.UI.MVC.Controllers
{
    public class SponsorTypesController : Controller
    {
        private readonly db_a88655_pumpkinrunContext _context;

        public SponsorTypesController(db_a88655_pumpkinrunContext context)
        {
            _context = context;
        }

        // GET: SponsorTypes
        public async Task<IActionResult> Index()
        {
            ViewData["Benefits"] = _context.Benefits.ToList();
            return _context.SponsorTypes != null ? 
                          View(await _context.SponsorTypes.Include(x => x.SponsorTypeBenefits).ThenInclude(x => x.Benefit).ToListAsync()) :
                          Problem("Entity set 'db_a88655_pumpkinrunContext.SponsorTypes'  is null.");
        }

        // GET: SponsorTypes/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SponsorTypes == null)
            {
                return NotFound();
            }

            var sponsorType = await _context.SponsorTypes
                .FirstOrDefaultAsync(m => m.SponsorTypeId == id);
            if (sponsorType == null)
            {
                return NotFound();
            }

            return View(sponsorType);
        }

        // GET: SponsorTypes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: SponsorTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SponsorTypeId,SponsorTypeName,Cost,NbrOffered")] SponsorType sponsorType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sponsorType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sponsorType);
        }

        // GET: SponsorTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SponsorTypes == null)
            {
                return NotFound();
            }

            var sponsorType = await _context.SponsorTypes.FindAsync(id);
            if (sponsorType == null)
            {
                return NotFound();
            }
            return View(sponsorType);
        }

        // POST: SponsorTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SponsorTypeId,SponsorTypeName,Cost,NbrOffered")] SponsorType sponsorType)
        {
            if (id != sponsorType.SponsorTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sponsorType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponsorTypeExists(sponsorType.SponsorTypeId))
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
            return View(sponsorType);
        }

        // GET: SponsorTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SponsorTypes == null)
            {
                return NotFound();
            }

            var sponsorType = await _context.SponsorTypes
                .FirstOrDefaultAsync(m => m.SponsorTypeId == id);
            if (sponsorType == null)
            {
                return NotFound();
            }

            return View(sponsorType);
        }

        // POST: SponsorTypes/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SponsorTypes == null)
            {
                return Problem("Entity set 'db_a88655_pumpkinrunContext.SponsorTypes'  is null.");
            }
            var sponsorType = await _context.SponsorTypes.FindAsync(id);
            if (sponsorType != null)
            {
                _context.SponsorTypes.Remove(sponsorType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SponsorTypeExists(int id)
        {
          return (_context.SponsorTypes?.Any(e => e.SponsorTypeId == id)).GetValueOrDefault();
        }
    }
}
