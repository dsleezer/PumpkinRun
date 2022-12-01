using System;
using System.Collections.Generic;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class RaceDayInformation
    {
        public int RaceDayId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string InstructionsFile { get; set; } = null!;
        public string? PacketPickupOption1 { get; set; }
        public string? PacketPickupOption2 { get; set; }
        public string? PacketPickupOption3 { get; set; }
        public DateTime? SchoolPickupDate { get; set; }
    }
}
