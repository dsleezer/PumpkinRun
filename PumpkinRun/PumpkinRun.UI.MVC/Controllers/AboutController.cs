using Microsoft.AspNetCore.Mvc;
using PumpkinRun.DATA.EF.Models;

namespace PumpkinRun.UI.MVC.Controllers
{
    public class AboutController : Controller
    {
        private readonly db_a88655_pumpkinrunContext _context;
        public AboutController(db_a88655_pumpkinrunContext context)
        {
            _context = context;
        }
        public IActionResult Info()
        {
            var raceDayInfo = _context.RaceDayInformations.FirstOrDefault();
            return View(raceDayInfo);
        }
        public IActionResult ChairsAndCommittee()
        {
            return View();
        }
        public async Task<IActionResult> FAQ()
        {
            var raceDayInfo = _context.RaceDayInformations.FirstOrDefault();
            return View(raceDayInfo);
        }
    }
}
