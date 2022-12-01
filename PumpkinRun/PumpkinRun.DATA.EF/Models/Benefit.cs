using System;
using System.Collections.Generic;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class Benefit
    {
        public Benefit()
        {
            SponsorTypeBenefits = new HashSet<SponsorTypeBenefit>();
        }

        public int BenefitId { get; set; }
        public string BenefitDescription { get; set; } = null!;

        public virtual ICollection<SponsorTypeBenefit> SponsorTypeBenefits { get; set; }
    }
}
