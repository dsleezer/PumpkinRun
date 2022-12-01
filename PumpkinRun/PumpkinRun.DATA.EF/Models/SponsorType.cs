using System;
using System.Collections.Generic;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class SponsorType
    {
        public SponsorType()
        {
            SponsorTypeBenefits = new HashSet<SponsorTypeBenefit>();
            Sponsors = new HashSet<Sponsor>();
        }

        public int SponsorTypeId { get; set; }
        public string SponsorTypeName { get; set; } = null!;
        public int Cost { get; set; }
        public byte NbrOffered { get; set; }

        public virtual ICollection<SponsorTypeBenefit> SponsorTypeBenefits { get; set; }
        public virtual ICollection<Sponsor> Sponsors { get; set; }
    }
}
