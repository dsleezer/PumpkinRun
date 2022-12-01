using PumpkinRun.DATA.EF.Models;

namespace PumpkinRun.UI.MVC.Models
{
    public class RaceViewModel
    {
        public RaceDayInformation RaceDayInfo { get; set; }
        public List<RaceResult> RaceResults { get; set; }
        public List<RaceVideo> RaceVideos { get; set; }
    }
}
