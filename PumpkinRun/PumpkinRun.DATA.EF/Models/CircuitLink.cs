using System;
using System.Collections.Generic;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class CircuitLink
    {
        public int CircuitLinkId { get; set; }
        public string CircuitName { get; set; } = null!;
        public string CircuitUrl { get; set; } = null!;
        public DateTime CircuitRaceDate { get; set; }
    }
}
