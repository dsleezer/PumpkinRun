using System;
using System.Collections.Generic;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class HomeRaceInfo
    {
        public int HomeRaceInfoId { get; set; }
        public DateTime PumpkinRunRaceDate { get; set; }
        public string RegistrationLink { get; set; } = null!;
    }
}
