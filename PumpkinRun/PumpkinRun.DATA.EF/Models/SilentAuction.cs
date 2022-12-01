using System;
using System.Collections.Generic;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class SilentAuction
    {
        public int Said { get; set; }
        public string? AuctionMessage { get; set; }
        public bool IsDisplayed { get; set; }
        public string? AuctionUrl { get; set; }
    }
}
