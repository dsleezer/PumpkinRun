using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PumpkinRun.DATA.EF.Models;
using PumpkinRun.UI.MVC.Utilities;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;

namespace PumpkinRun.UI.MVC.Controllers
{
    public class SponsorsController : Controller
    {
        private readonly db_a88655_pumpkinrunContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SponsorsController(db_a88655_pumpkinrunContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Sponsors
        public async Task<IActionResult> Index()
        {
            var db_a88655_pumpkinrunContext = _context.Sponsors.Include(s => s.SponsorType);
            ViewData["SponsorTypes"] = await _context.SponsorTypes.ToListAsync();
            return View(await db_a88655_pumpkinrunContext.ToListAsync());
        }

        // GET: Sponsors for Manage Page
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Manage()
        {
            var db_a88655_pumpkinrunContext = _context.Sponsors.Include(s => s.SponsorType);
            ViewData["SponsorTypes"] = _context.SponsorTypes.ToList();
            return View(await db_a88655_pumpkinrunContext.OrderBy(s => s.SponsorName).ToListAsync());
        }

        public async Task<IActionResult> Beneficiaries()
        {
            return View();
        }

        public async Task<IActionResult> SponsorMessages()
        {
            return View();
        }

        // GET: Sponsors/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["SponsorTypeId"] = new SelectList(_context.SponsorTypes, "SponsorTypeId", "SponsorTypeName");
            return View();
        }

        // POST: Sponsors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("SponsorId,SponsorName,SponsorUrl,SponsorLogo,SponsorTypeId,SponsorVideoUrl, Image")] Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                #region File Upload - CREATE
                //check to see if file was uploaded

                if (sponsor.Image != null)
                {                    
                    string ext = Path.GetExtension(sponsor.Image.FileName);
                    string[] validExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    //- verify the uploaded file has an extension in above list
                    //- also verify file size is small enough
                    if (validExts.Contains(ext.ToLower()) && sponsor.Image.Length < 4_194_303)
                    {
                        //generate a unique filename
                        sponsor.SponsorLogo = Guid.NewGuid() + ext;

                        //save the file to the web server (here, saving to wwwroot/images)
                        string webRootPath = _webHostEnvironment.WebRootPath;

                        string fullImagePath = webRootPath + "/assets/images/Sponsors/";

                        //read the image into the server memory
                        using (var memoryStream = new MemoryStream())
                        {
                            await sponsor.Image.CopyToAsync(memoryStream);//transfer file from request to server memory
                            using (var img = Image.FromStream(memoryStream)) //add using statement for system.drawing
                            {
                                //now, send the image to the ImageUtility for resizing and thumbnail creation
                                //items needed for the ImageUtility.ResizeImage()
                                //1) (int) maximum image size
                                //2) (int) maximum thumbnail image size
                                //3) (string) full path where the file will be saved
                                //4) (Image) an image
                                //5) (string) filename
                                int maxImageSize = 500; //in pixels
                                int maxThumbSize = 100;

                                ImageUtility.ResizeImage(fullImagePath, sponsor.SponsorLogo, img, maxImageSize, maxThumbSize);
                            }
                        }
                    }
                }

                else
                {
                    //if no image was uploaded, assign default filename
                    //will also need to download a default image and name it noimage.png
                    sponsor.SponsorLogo = null;
                }

                #endregion

                _context.Add(sponsor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SponsorTypeId"] = new SelectList(_context.SponsorTypes, "SponsorTypeId", "SponsorTypeName", sponsor.SponsorTypeId);
            return View(sponsor);
        }

        // GET: Sponsors/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sponsors == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }
            ViewData["SponsorTypeId"] = new SelectList(_context.SponsorTypes, "SponsorTypeId", "SponsorTypeName", sponsor.SponsorTypeId);
            return View(sponsor);
        }

        // POST: Sponsors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("SponsorId,SponsorName,SponsorUrl,SponsorLogo,SponsorTypeId,SponsorVideoUrl, Image")] Sponsor sponsor)
        {
            if (id != sponsor.SponsorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    #region File Upload - EDIT
                    string oldImageName = sponsor.SponsorLogo;

                    //Check to see if a file was uploaded
                    if (sponsor.Image != null)
                    {
                        //Check the file type 
                        //- retrieve the extension of the uploaded file
                        string ext = Path.GetExtension(sponsor.Image.FileName);

                        //- Create a list of valid extensions to check against
                        string[] validExts = { ".jpeg", ".jpg", ".gif", ".png" };

                        //- verify the uploaded file has an extension matching one of the extensions in the list above
                        //- AND verify file size will work with our .NET app
                        if (validExts.Contains(ext.ToLower()) && sponsor.Image.Length < 4_194_303)//underscores don't change the number, they just make it easier to read
                        {
                            //Generate a unique filename
                            sponsor.SponsorLogo = Guid.NewGuid() + ext;

                            //Save the file to the web server (here, saving to wwwroot/images)
                            //To access wwwroot, add a property to the controller for the _webHostEnvironment (see the top of this class for our example)
                            //Retrieve the path to wwwroot
                            string webRootPath = _webHostEnvironment.WebRootPath;
                            //variable for the full image path --> this is where we will save the image
                            string fullImagePath = webRootPath + "/assets/images/Sponsors/";

                            //Delete the old image
                            if (oldImageName != null)
                            {
                                ImageUtility.Delete(fullImagePath, oldImageName);
                            }

                            //Create a MemoryStream to read the image into the server memory
                            using (var memoryStream = new MemoryStream())
                            {
                                await sponsor.Image.CopyToAsync(memoryStream);//transfer file from the request to server memory
                                using (var img = Image.FromStream(memoryStream))//add a using statement for the Image class (using System.Drawing)
                                {
                                    //now, send the image to the ImageUtility for resizing and thumbnail creation
                                    //items needed for the ImageUtility.ResizeImage()
                                    //1) (int) maximum image size
                                    //2) (int) maximum thumbnail image size
                                    //3) (string) full path where the file will be saved
                                    //4) (Image) an image
                                    //5) (string) filename
                                    int maxImageSize = 500;//in pixels
                                    int maxThumbSize = 100;

                                    ImageUtility.ResizeImage(fullImagePath, sponsor.SponsorLogo, img, maxImageSize, maxThumbSize);
                                    //myFile.Save("path/to/folder", "filename"); - how to save something that's NOT an image

                                }
                            }
                        }
                    }
                    #endregion

                    _context.Update(sponsor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponsorExists(sponsor.SponsorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Manage));
            }
            ViewData["SponsorTypeId"] = new SelectList(_context.SponsorTypes, "SponsorTypeId", "SponsorTypeName", sponsor.SponsorTypeId);
            return View(sponsor);
        }

        // GET: Sponsors/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sponsors == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors
                .Include(s => s.SponsorType)
                .FirstOrDefaultAsync(m => m.SponsorId == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }

        // POST: Sponsors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sponsors == null)
            {
                return Problem("Entity set 'db_a88655_pumpkinrunContext.Sponsors'  is null.");
            }
            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor != null)
            {
                _context.Sponsors.Remove(sponsor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SponsorExists(int id)
        {
          return (_context.Sponsors?.Any(e => e.SponsorId == id)).GetValueOrDefault();
        }
    }
}
