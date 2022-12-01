using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PumpkinRun.DATA.EF.Models;
using PumpkinRun.UI.MVC.Models;
using PumpkinRun.UI.MVC.Utilities;
using System.Diagnostics;
using System.Drawing;

namespace PumpkinRun.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly db_a88655_pumpkinrunContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public HomeController(ILogger<HomeController> logger, db_a88655_pumpkinrunContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task<IActionResult> Index()
        {
            var raceDayInfo = _context.RaceDayInformations.SingleOrDefault();
            var circuitLinks = _context.CircuitLinks.ToList();
            var silentAuction = _context.SilentAuctions.SingleOrDefault();
            var messageFromTom = _context.MessageFromToms.SingleOrDefault();
            var homeRaceInfo = _context.HomeRaceInfos.SingleOrDefault();

            HomeIndexViewModel hivm = new HomeIndexViewModel()
            {
                RaceDayInfo = raceDayInfo,
                CircuitLinks = circuitLinks,
                SilentAuction = silentAuction,
                MessageFromTom = messageFromTom,
                HomeRaceInfo = homeRaceInfo
            };

            return View(hivm);
        }

        public async Task<IActionResult> TheRace()
        {
            var raceDayInfo = _context.RaceDayInformations.SingleOrDefault();
            var raceResults = _context.RaceResults.ToList();
            var raceVideos = _context.RaceVideos.ToList();

            RaceViewModel trvm = new RaceViewModel()
            {
                RaceDayInfo = raceDayInfo,
                RaceResults = raceResults,
                RaceVideos = raceVideos
            };

            return View(trvm);
        }

        // GET: RaceResults/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRace(int? id)
        {
            if (id == null || _context.RaceDayInformations == null)
            {
                return NotFound();
            }

            var raceDayInfo = await _context.RaceDayInformations.FindAsync(id);
            if (raceDayInfo == null)
            {
                return NotFound();
            }
            return View(raceDayInfo);
        }

        // POST: RaceResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRace(int id, [Bind("RaceDayId,ScheduledDate,InstructionsFile,PacketPickupOption1,PacketPickupOption2,PacketPickupOption3,SchoolPickupDate,InstructionsFlyer")] RaceDayInformation raceDayInfo)
        {
            if (id != raceDayInfo.RaceDayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                #region EDIT File Upload                
                string oldFileName = raceDayInfo.InstructionsFile;

                //Check if the user uploaded a file
                if (raceDayInfo.InstructionsFlyer != null)
                {
                    //get the file's extension
                    string ext = Path.GetExtension(raceDayInfo.InstructionsFlyer.FileName);

                    //list valid extensions
                    string[] validExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //check the file's extension against the list of valid extensions
                    if (validExts.Contains(ext.ToLower()) && raceDayInfo.InstructionsFlyer.Length < 4_194_303)
                    {
                        //generate a unique file name
                        raceDayInfo.InstructionsFile = Guid.NewGuid() + ext;
                        //build our file path to save the image
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullPath = webRootPath + "/assets/docs/" + raceDayInfo.InstructionsFile;

                        //Delete the old image
                        if (oldFileName != "noimage.png")
                        {
                            ImageUtility.Delete(fullPath, oldFileName);
                        }

                        //Save the new image to webroot
                        using (var memoryStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await raceDayInfo.InstructionsFlyer.CopyToAsync(memoryStream);                            
                        }

                    }
                }
                #endregion


                try
                {
                    _context.Update(raceDayInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceDayInfoExists(raceDayInfo.RaceDayId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(raceDayInfo);
        }

        private bool RaceDayInfoExists(int id)
        {
            return (_context.RaceDayInformations?.Any(e => e.RaceDayId == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> ToggleSilentAuction()
        {
            if (_context.SilentAuctions == null)
            {
                return NotFound();
            }
            var silentAuction = await _context.SilentAuctions.SingleOrDefaultAsync();
            if (silentAuction == null)
            {
                return NotFound();
            }
            if (!silentAuction.IsDisplayed)
            {
                silentAuction.IsDisplayed = true;
            }
            else
            {
                silentAuction.IsDisplayed = false;
            }

            _context.Update(silentAuction);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        // GET: CircuitLinks Edit
        public async Task<IActionResult> EditSilentAuction(int? id)
        {
            if (id == null || _context.SilentAuctions == null)
            {
                return NotFound();
            }

            var silentAuction = await _context.SilentAuctions.FindAsync(id);
            if (silentAuction == null)
            {
                return NotFound();
            }


            return View(silentAuction);

        }

        [Authorize(Roles = "Admin")]
        // POST: CircuitLinks Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSilentAuction(int id, [Bind("Said,AuctionMessage,IsDisplayed,AuctionUrl")] SilentAuction silentAuction)
        {
            if (id != silentAuction.Said)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(silentAuction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CircuitLinkExists(silentAuction.Said))
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
            return View(silentAuction);
        }





        [Authorize(Roles = "Admin")]
        // GET: CircuitLinks Edit
        public async Task<IActionResult> EditCircuitLinks(int? id)
        {
            if (id == null || _context.MessageFromToms == null)
            {
                return NotFound();
            }

            var circuitLinks = await _context.CircuitLinks.FindAsync(id);
            if (circuitLinks == null)
            {
                return NotFound();
            }


            return View(circuitLinks);

        }

        [Authorize(Roles = "Admin")]
        // POST: CircuitLinks Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCircuitLinks(int id, [Bind("CircuitLinkId,CircuitName,CircuitUrl,CircuitRaceDate")] CircuitLink circuitLinks)
        {
            if (id != circuitLinks.CircuitLinkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(circuitLinks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CircuitLinkExists(circuitLinks.CircuitLinkId))
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
            return View(circuitLinks);
        }







        [Authorize(Roles = "Admin")]
        // GET: Tom Message Edit
        public async Task<IActionResult> EditTomMessage(int? id)
        {
            if (id == null || _context.MessageFromToms == null)
            {
                return NotFound();
            }

            var messageFromTom = await _context.MessageFromToms.FindAsync(id);
            if (messageFromTom == null)
            {
                return NotFound();
            }


            return View(messageFromTom);


        }

        [Authorize(Roles = "Admin")]
        // POST: Tom Message Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTomMessage(int id, [Bind("MessageId,MessageContent,Photo1,Photo2,Photo3,Photo4,Image1,Image2,Image3,Image4")] MessageFromTom messageFromTom)
        {
            if (id != messageFromTom.MessageId)
            {
                return NotFound();
            }

            #region Photo Upload

            string oldImage1 = messageFromTom.Photo1;
            string oldImage2 = messageFromTom.Photo2;
            string oldImage3 = messageFromTom.Photo3;
            string oldImage4 = messageFromTom.Photo4;

            string[] imageNames = new string[]{
                messageFromTom.Photo1,
                messageFromTom.Photo2,
                messageFromTom.Photo3,
                messageFromTom.Photo4,
            };

            List<IFormFile> images = new List<IFormFile>()
            {
                messageFromTom.Image1,
                messageFromTom.Image2,
                messageFromTom.Image3,
                messageFromTom.Image4
            };

            for (int i = 0; i <= 3; i++)
            {
                if (images[i] != null)
                {
                    string ext = Path.GetExtension(images[i].FileName);
                    string[] validExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    if (validExts.Contains(ext.ToLower()))
                    {
                        imageNames[i] = Guid.NewGuid() + ext;

                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullPath = webRootPath + "/assets/images/";

                        if (imageNames[i] != "noimage.png")
                        {
                            ImageUtility.Delete(fullPath, imageNames[i]);
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            await images[i].CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;
                                ImageUtility.ResizeImage(fullPath, imageNames[i], img, maxImageSize, maxThumbSize);
                            }
                        }
                    }
                }
            }
            if (images[0] != null || images[1] != null || images[2] != null || images[3] != null)
            {
                messageFromTom.Photo1 = imageNames[0];
                messageFromTom.Photo2 = imageNames[1];
                messageFromTom.Photo3 = imageNames[2];
                messageFromTom.Photo4 = imageNames[3];
            }

            #endregion

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageFromTom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageFromTomExists(messageFromTom.MessageId))
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

            return View(messageFromTom);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult HowToHelp()
        {
            return View();
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
            return RedirectToAction("HowToHelp");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private bool MessageFromTomExists(int id)
        {
            return (_context.MessageFromToms?.Any(e => e.MessageId == id)).GetValueOrDefault();
        }
        private bool CircuitLinkExists(int id)
        {
            return (_context.CircuitLinks?.Any(e => e.CircuitLinkId == id)).GetValueOrDefault();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}