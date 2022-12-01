using PumpkinRun.DATA.EF.Models;

namespace PumpkinRun.UI.MVC.Models
{
    public class HomeIndexViewModel
    {
        public RaceDayInformation RaceDayInfo { get; set; }

        public List<CircuitLink> CircuitLinks { get; set; }

        public SilentAuction SilentAuction { get; set; }

        public MessageFromTom MessageFromTom { get; set; }

        public HomeRaceInfo HomeRaceInfo { get; set; }
        
    }
}
