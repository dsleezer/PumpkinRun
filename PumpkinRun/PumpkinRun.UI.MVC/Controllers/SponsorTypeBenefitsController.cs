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
    public class SponsorTypeBenefitsController : Controller
    {
        private readonly db_a88655_pumpkinrunContext _context;

        public SponsorTypeBenefitsController(db_a88655_pumpkinrunContext context)
        {
            _context = context;
        }

        // GET: SponsorTypeBenefits
        public async Task<IActionResult> Index()
        {
            var db_a88655_pumpkinrunContext = _context.SponsorTypeBenefits.Include(s => s.Benefit).Include(s => s.SponsorType);
            return View(await db_a88655_pumpkinrunContext.ToListAsync());
        }

        // GET: SponsorTypeBenefits/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SponsorTypeBenefits == null)
            {
                return NotFound();
            }

            var sponsorTypeBenefit = await _context.SponsorTypeBenefits
                .Include(s => s.Benefit)
                .Include(s => s.SponsorType)
                .FirstOrDefaultAsync(m => m.Stbid == id);
            if (sponsorTypeBenefit == null)
            {
                return NotFound();
            }

            return View(sponsorTypeBenefit);
        }

        // GET: SponsorTypeBenefits/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["BenefitId"] = new SelectList(_context.Benefits, "BenefitId", "BenefitDescription");
            ViewData["SponsorTypeId"] = new SelectList(_context.SponsorTypes, "SponsorTypeId", "SponsorTypeName");
            return View();
        }

        // POST: SponsorTypeBenefits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Stbid,SponsorTypeId,BenefitId")] SponsorTypeBenefit sponsorTypeBenefit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sponsorTypeBenefit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BenefitId"] = new SelectList(_context.Benefits, "BenefitId", "BenefitDescription", sponsorTypeBenefit.BenefitId);
            ViewData["SponsorTypeId"] = new SelectList(_context.SponsorTypes, "SponsorTypeId", "SponsorTypeName", sponsorTypeBenefit.SponsorTypeId);
            return View(sponsorTypeBenefit);
        }

        // GET: SponsorTypeBenefits/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SponsorTypeBenefits == null)
            {
                return NotFound();
            }

            var sponsorTypeBenefit = await _context.SponsorTypeBenefits.FindAsync(id);
            if (sponsorTypeBenefit == null)
            {
                return NotFound();
            }
            ViewData["BenefitId"] = new SelectList(_context.Benefits, "BenefitId", "BenefitDescription", sponsorTypeBenefit.BenefitId);
            ViewData["SponsorTypeId"] = new SelectList(_context.SponsorTypes, "SponsorTypeId", "SponsorTypeName", sponsorTypeBenefit.SponsorTypeId);
            return View(sponsorTypeBenefit);
        }

        // POST: SponsorTypeBenefits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Stbid,SponsorTypeId,BenefitId")] SponsorTypeBenefit sponsorTypeBenefit)
        {
            if (id != sponsorTypeBenefit.Stbid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sponsorTypeBenefit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponsorTypeBenefitExists(sponsorTypeBenefit.Stbid))
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
            ViewData["BenefitId"] = new SelectList(_context.Benefits, "BenefitId", "BenefitDescription", sponsorTypeBenefit.BenefitId);
            ViewData["SponsorTypeId"] = new SelectList(_context.SponsorTypes, "SponsorTypeId", "SponsorTypeName", sponsorTypeBenefit.SponsorTypeId);
            return View(sponsorTypeBenefit);
        }

        // GET: SponsorTypeBenefits/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SponsorTypeBenefits == null)
            {
                return NotFound();
            }

            var sponsorTypeBenefit = await _context.SponsorTypeBenefits
                .Include(s => s.Benefit)
                .Include(s => s.SponsorType)
                .FirstOrDefaultAsync(m => m.Stbid == id);
            if (sponsorTypeBenefit == null)
            {
                return NotFound();
            }

            return View(sponsorTypeBenefit);
        }

        // POST: SponsorTypeBenefits/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SponsorTypeBenefits == null)
            {
                return Problem("Entity set 'db_a88655_pumpkinrunContext.SponsorTypeBenefits'  is null.");
            }
            var sponsorTypeBenefit = await _context.SponsorTypeBenefits.FindAsync(id);
            if (sponsorTypeBenefit != null)
            {
                _context.SponsorTypeBenefits.Remove(sponsorTypeBenefit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SponsorTypeBenefitExists(int id)
        {
          return (_context.SponsorTypeBenefits?.Any(e => e.Stbid == id)).GetValueOrDefault();
        }
    }
}
