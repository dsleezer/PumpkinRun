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
    public class RaceResultsController : Controller
    {
        private readonly db_a88655_pumpkinrunContext _context;

        public RaceResultsController(db_a88655_pumpkinrunContext context)
        {
            _context = context;
        }

        // GET: RaceResults
        
        public async Task<IActionResult> Index()
        {
            var maleRecordTime = _context.RaceResults.Min(x => x.MaleWinner1Time);
            ViewData["MaleRecordTime"] = maleRecordTime;
            ViewData["MaleRecordName"] = _context.RaceResults.Where(x => x.MaleWinner1Time == maleRecordTime).SingleOrDefault();

            var femaleRecordTime = _context.RaceResults.Min(x => x.FemaleWinner1Time);
            ViewData["FemaleRecordTime"] = femaleRecordTime;
            ViewData["FemaleRecordName"] = _context.RaceResults.Where(x => x.FemaleWinner1Time == femaleRecordTime).SingleOrDefault();

            ViewData["Race Videos"] = _context.RaceVideos.ToList();
              return _context.RaceResults != null ? 
                          View(await _context.RaceResults.OrderByDescending(x => x.RaceYear).ToListAsync()) :
                          Problem("Entity set 'db_a88655_pumpkinrunContext.RaceResults'  is null.");
        }

        // GET: RaceResults/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RaceResults == null)
            {
                return NotFound();
            }

            var raceResult = await _context.RaceResults
                .FirstOrDefaultAsync(m => m.RaceResultId == id);
            if (raceResult == null)
            {
                return NotFound();
            }

            return View(raceResult);
        }

        // GET: RaceResults/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: RaceResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RaceResultId,RaceYear,MaleWinner1Name,MaleWinner1Time,MaleWinner2Name,MaleWinner2Time,MaleWinner3Name,MaleWinner3Time,FemaleWinner1Name,FemaleWinner1Time,FemalWinner2Name,FemaleWinner2Time,FemaleWinner3Name,FemaleWinner3Time,ExternalResultsUrl,RacePhotosUrl")] RaceResult raceResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raceResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(raceResult);
        }

        // GET: RaceResults/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RaceResults == null)
            {
                return NotFound();
            }

            var raceResult = await _context.RaceResults.FindAsync(id);
            if (raceResult == null)
            {
                return NotFound();
            }
            return View(raceResult);
        }

        // POST: RaceResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RaceResultId,RaceYear,MaleWinner1Name,MaleWinner1Time,MaleWinner2Name,MaleWinner2Time,MaleWinner3Name,MaleWinner3Time,FemaleWinner1Name,FemaleWinner1Time,FemalWinner2Name,FemaleWinner2Time,FemaleWinner3Name,FemaleWinner3Time,ExternalResultsUrl,RacePhotosUrl")] RaceResult raceResult)
        {
            if (id != raceResult.RaceResultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raceResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceResultExists(raceResult.RaceResultId))
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
            return View(raceResult);
        }

        // GET: RaceResults/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RaceResults == null)
            {
                return NotFound();
            }

            var raceResult = await _context.RaceResults
                .FirstOrDefaultAsync(m => m.RaceResultId == id);
            if (raceResult == null)
            {
                return NotFound();
            }

            return View(raceResult);
        }

        // POST: RaceResults/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RaceResults == null)
            {
                return Problem("Entity set 'db_a88655_pumpkinrunContext.RaceResults'  is null.");
            }
            var raceResult = await _context.RaceResults.FindAsync(id);
            if (raceResult != null)
            {
                _context.RaceResults.Remove(raceResult);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaceResultExists(int id)
        {
          return (_context.RaceResults?.Any(e => e.RaceResultId == id)).GetValueOrDefault();
        }
    }
}
