using System;
using System.Collections.Generic;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class Sponsor
    {
        public int SponsorId { get; set; }
        public string SponsorName { get; set; } = null!;
        public string? SponsorUrl { get; set; }
        public string? SponsorLogo { get; set; }
        public int SponsorTypeId { get; set; }
        public string? SponsorVideoUrl { get; set; }

        public virtual SponsorType? SponsorType { get; set; }
    }
}
