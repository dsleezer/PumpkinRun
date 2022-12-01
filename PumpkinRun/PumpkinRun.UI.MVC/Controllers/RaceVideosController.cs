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
    public class RaceVideosController : Controller
    {
        private readonly db_a88655_pumpkinrunContext _context;

        public RaceVideosController(db_a88655_pumpkinrunContext context)
        {
            _context = context;
        }

        // GET: RaceVideos
        public async Task<IActionResult> Index()
        {
              return _context.RaceVideos != null ? 
                          View(await _context.RaceVideos.ToListAsync()) :
                          Problem("Entity set 'db_a88655_pumpkinrunContext.RaceVideos'  is null.");
        }

        // GET: RaceVideos/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RaceVideos == null)
            {
                return NotFound();
            }

            var raceVideo = await _context.RaceVideos
                .FirstOrDefaultAsync(m => m.RaceVideoId == id);
            if (raceVideo == null)
            {
                return NotFound();
            }

            return View(raceVideo);
        }

        // GET: RaceVideos/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: RaceVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("RaceVideoId,RaceYear,VideoTitle,VideoUrl")] RaceVideo raceVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raceVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(raceVideo);
        }

        // GET: RaceVideos/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RaceVideos == null)
            {
                return NotFound();
            }

            var raceVideo = await _context.RaceVideos.FindAsync(id);
            if (raceVideo == null)
            {
                return NotFound();
            }
            return View(raceVideo);
        }

        // POST: RaceVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RaceVideoId,RaceYear,VideoTitle,VideoUrl")] RaceVideo raceVideo)
        {
            if (id != raceVideo.RaceVideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raceVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceVideoExists(raceVideo.RaceVideoId))
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
            return View(raceVideo);
        }

        // GET: RaceVideos/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RaceVideos == null)
            {
                return NotFound();
            }

            var raceVideo = await _context.RaceVideos
                .FirstOrDefaultAsync(m => m.RaceVideoId == id);
            if (raceVideo == null)
            {
                return NotFound();
            }

            return View(raceVideo);
        }

        // POST: RaceVideos/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RaceVideos == null)
            {
                return Problem("Entity set 'db_a88655_pumpkinrunContext.RaceVideos'  is null.");
            }
            var raceVideo = await _context.RaceVideos.FindAsync(id);
            if (raceVideo != null)
            {
                _context.RaceVideos.Remove(raceVideo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaceVideoExists(int id)
        {
          return (_context.RaceVideos?.Any(e => e.RaceVideoId == id)).GetValueOrDefault();
        }
    }
}
