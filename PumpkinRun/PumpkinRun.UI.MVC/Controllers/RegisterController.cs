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
    public class RegisterController : Controller
    {
        private readonly db_a88655_pumpkinrunContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RegisterController(db_a88655_pumpkinrunContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            ViewData["RegisterLink"] = _context.HomeRaceInfos.Select(x => x.RegistrationLink).SingleOrDefault();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRegisterLink()
        {
            var homeInfo = _context.HomeRaceInfos.SingleOrDefault();
            if (homeInfo == null)
            {
                return NotFound();
            }

            return View(homeInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRegisterLink([Bind("HomeRaceInfoId,PumpkinRunRaceDate,RegistrationLink")] HomeRaceInfo hri)
        {
            if (ModelState.IsValid)
            {

                _context.Update(hri);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(hri);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult UploadIndividual(IFormFile individualForm)
        {
            //handle form submission
            if (individualForm != null)
            {
                string ext = Path.GetExtension(individualForm.FileName);

                string[] validExts = { ".pdf", ".docx", ".doc" };

                if (validExts.Contains(ext.ToLower()))
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;

                    string fullPdfPath = webRootPath + "/assets/pdf/Registration-Form-Individual.pdf";

                    using (var stream = System.IO.File.Create(fullPdfPath))
                    {
                        individualForm.CopyTo(stream);
                    }
                }
                ViewData["ErrorMessage"] = null;
            }
            else
            {
                ViewData["ErrorMessage"] = "Something went wrong.";
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UploadClub(IFormFile clubForm)
        {
            //handle form submission
            if (clubForm != null)
            {
                string ext = Path.GetExtension(clubForm.FileName);

                string[] validExts = { ".pdf", ".docx", ".doc" };

                if (validExts.Contains(ext.ToLower()))
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;

                    string fullPdfPath = webRootPath + "/assets/pdf/Registration-Form-School.pdf";

                    using (var stream = System.IO.File.Create(fullPdfPath))
                    {
                        clubForm.CopyTo(stream);
                    }
                }
                ViewData["ErrorMessage"] = null;
            }
            else
            {
                ViewData["ErrorMessage"] = "Something went wrong.";
            }
            return RedirectToAction("Index");
        }
    }
}
