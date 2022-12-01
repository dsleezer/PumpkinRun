using System;
using System.Collections.Generic;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class RaceVideo
    {
        public int RaceVideoId { get; set; }
        public DateTime RaceYear { get; set; }
        public string? VideoTitle { get; set; }
        public string VideoUrl { get; set; } = null!;
    }
}
