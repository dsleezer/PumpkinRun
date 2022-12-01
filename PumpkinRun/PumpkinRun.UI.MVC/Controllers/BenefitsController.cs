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
    public class BenefitsController : Controller
    {
        private readonly db_a88655_pumpkinrunContext _context;

        public BenefitsController(db_a88655_pumpkinrunContext context)
        {
            _context = context;
        }

        // GET: Benefits
        public async Task<IActionResult> Index()
        {
              return _context.Benefits != null ? 
                          View(await _context.Benefits.ToListAsync()) :
                          Problem("Entity set 'db_a88655_pumpkinrunContext.Benefits'  is null.");
        }

        // GET: Benefits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Benefits == null)
            {
                return NotFound();
            }

            var benefit = await _context.Benefits
                .FirstOrDefaultAsync(m => m.BenefitId == id);
            if (benefit == null)
            {
                return NotFound();
            }

            return View(benefit);
        }

        [Authorize(Roles = "Admin")]
        // GET: Benefits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Benefits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BenefitId,BenefitDescription")] Benefit benefit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(benefit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(benefit);
        }

        // GET: Benefits/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Benefits == null)
            {
                return NotFound();
            }

            var benefit = await _context.Benefits.FindAsync(id);
            if (benefit == null)
            {
                return NotFound();
            }
            return View(benefit);
        }

        // POST: Benefits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BenefitId,BenefitDescription")] Benefit benefit)
        {
            if (id != benefit.BenefitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(benefit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BenefitExists(benefit.BenefitId))
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
            return View(benefit);
        }

        // GET: Benefits/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Benefits == null)
            {
                return NotFound();
            }

            var benefit = await _context.Benefits
                .FirstOrDefaultAsync(m => m.BenefitId == id);
            if (benefit == null)
            {
                return NotFound();
            }

            return View(benefit);
        }

        // POST: Benefits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Benefits == null)
            {
                return Problem("Entity set 'db_a88655_pumpkinrunContext.Benefits'  is null.");
            }
            var benefit = await _context.Benefits.FindAsync(id);
            if (benefit != null)
            {
                _context.Benefits.Remove(benefit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BenefitExists(int id)
        {
          return (_context.Benefits?.Any(e => e.BenefitId == id)).GetValueOrDefault();
        }
    }
}
