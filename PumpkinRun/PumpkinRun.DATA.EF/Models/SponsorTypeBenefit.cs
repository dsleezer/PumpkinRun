using System;
using System.Collections.Generic;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class SponsorTypeBenefit
    {
        public int Stbid { get; set; }
        public int SponsorTypeId { get; set; }
        public int BenefitId { get; set; }

        public virtual Benefit? Benefit { get; set; }
        public virtual SponsorType? SponsorType { get; set; }
    }
}
