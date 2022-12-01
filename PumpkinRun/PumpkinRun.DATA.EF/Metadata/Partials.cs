using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpkinRun.DATA.EF.Models
{
    [ModelMetadataType(typeof(BenefitMetadata))]
    public partial class Benefit { }

    [ModelMetadataType(typeof(CircuitLinkMetadata))]
    public partial class CircuitLink { }

    [ModelMetadataType(typeof(HomeRaceInfoMetadata))]
    public partial class HomeRaceInfo { }

    [ModelMetadataType(typeof(MessageFromTomMetadata))]
    public partial class MessageFromTom 
    {
        [NotMapped]
        public IFormFile? Image1 { get; set; }
        [NotMapped]
        public IFormFile? Image2 { get; set; }
        [NotMapped]
        public IFormFile? Image3 { get; set; }
        [NotMapped]
        public IFormFile? Image4 { get; set; }
    }

    [ModelMetadataType(typeof(RaceDayInformationMetadata))]
    public partial class RaceDayInformation 
    {
        [NotMapped]
        public IFormFile? InstructionsFlyer { get; set; }
    }

    [ModelMetadataType(typeof(RaceResultMetadata))]
    public partial class RaceResult { }

    [ModelMetadataType(typeof(RaceVideoMetadata))]
    public partial class RaceVideo { }

    [ModelMetadataType(typeof(SponsorMetadata))]
    public partial class Sponsor 
    {
        [NotMapped]
        public IFormFile? Image { get; set; }
    }

    [ModelMetadataType(typeof(SponsorTypeMetadata))]
    public partial class SponsorType { }

    [ModelMetadataType(typeof(SponsorTypeBenefitMetadata))]
    public partial class SponsorTypeBenefit { }

}
