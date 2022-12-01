using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpkinRun.DATA.EF.Models
{
    public class BenefitMetadata
    {
        public int BenefitId { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(250, ErrorMessage = "Description must not exceed 250 characters.")]
        [Display(Name = "Description")]
        public string BenefitDescription { get; set; }
    }
    public class CircuitLinkMetadata
    {
        public int CircuitLinkId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string CircuitName { get; set; }
        [StringLength(100, ErrorMessage = "URL must not exceed 100 characters.")]
        [Required(ErrorMessage = "URL is required.")]
        public string CircuitUrl { get; set; }
        [Required(ErrorMessage = "Race Date is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime CircuitRaceDate { get; set; }
    }
    public class HomeRaceInfoMetadata
    {
        public int HomeRaceInfoId { get; set; }
        [Required(ErrorMessage = "Race Date is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime PumpkinRunRaceDate { get; set; }
        [Required(ErrorMessage = "Registration Link is required.")]
        [DataType(DataType.Url)]
        [StringLength(75, ErrorMessage = "Registration Link must not exceed 75 characters.")]
        public string RegistrationLink { get; set; }
    }
    public class MessageFromTomMetadata
    {
        public int MessageId { get; set; }
        [Required(ErrorMessage = "Message is required.")]        
        public string MessageContent { get; set; }
        [StringLength(75, ErrorMessage = "Image Path must not exceed 75 characters.")]
        public string Photo1 { get; set; }
        [StringLength(75, ErrorMessage = "Image Path must not exceed 75 characters.")]
        public string Photo2 { get; set; }
        [StringLength(75, ErrorMessage = "Image Path must not exceed 75 characters.")]
        public string Photo3 { get; set; }
        [StringLength(75, ErrorMessage = "Image Path must not exceed 75 characters.")]
        public string Photo4 { get; set; }        
    }
    public class RaceDayInformationMetadata
    {
        public int RaceDayId { get; set; }
        [Required(ErrorMessage = "Date is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }
        [Required(ErrorMessage = "Instructions File is required.")]
        [StringLength(75, ErrorMessage = "Instructions File must not exceed 75 characters.")]
        public string InstructionsFile { get; set; }
        [StringLength(100, ErrorMessage = "Packet Pickup Option must not exceed 100 characters.")]
        public string PacketPickupOption1 { get; set; }
        [StringLength(100, ErrorMessage = "Packet Pickup Option must not exceed 100 characters.")]
        public string PacketPickupOption2 { get; set; }
        [StringLength(100, ErrorMessage = "Packet Pickup Option must not exceed 100 characters.")]
        public string PacketPickupOption3 { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime SchoolPickupDate { get; set; }
    }
    public class RaceResultMetadata
    {
        public int RaceResultId { get; set; }
        [Required(ErrorMessage = "Race Year is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        //[DataType(DataType.Date)]
        public DateTime RaceYear { get; set; }
        [Required(ErrorMessage = "Name for Male Winner #1 is required.")]
        [StringLength(75, ErrorMessage = "Name must not exceed 75 characters.")]
        public string MaleWinner1Name { get; set; }
        [Required(ErrorMessage = "Time for Male Winner #1 is required.")]
        [DisplayFormat(DataFormatString = "{0:mm\\:ss}")]
        [DataType(DataType.Time)]
        public TimeSpan MaleWinner1Time { get; set; }
        [Required(ErrorMessage = "Name for Male Winner #2 is required.")]
        [StringLength(75, ErrorMessage = "Name must not exceed 75 characters.")]
        public string MaleWinner2Name { get; set; }
        [Required(ErrorMessage = "Time for Male Winner #2 is required.")]
        [DisplayFormat(DataFormatString = "{0:mm\\:ss}")]
        [DataType(DataType.Time)]
        public TimeSpan MaleWinner2Time { get; set; }
        [Required(ErrorMessage = "Name for Male Winner #3 is required.")]
        [StringLength(75, ErrorMessage = "Name must not exceed 75 characters.")]
        public string MaleWinner3Name { get; set; }
        [Required(ErrorMessage = "Time for Male Winner #3 is required.")]
        [DisplayFormat(DataFormatString = "{0:mm\\:ss}")]
        [DataType(DataType.Time)]
        public TimeSpan MaleWinner3Time { get; set; }
        [Required(ErrorMessage = "Name for Female Winner #1 is required.")]
        [StringLength(75, ErrorMessage = "Name must not exceed 75 characters.")]
        public string FemaleWinner1Name { get; set; }
        [Required(ErrorMessage = "Time for Female Winner #1 is required.")]
        [DisplayFormat(DataFormatString = "{0:mm\\:ss}")]
        [DataType(DataType.Time)]
        public TimeSpan FemaleWinner1Time { get; set; }
        [Required(ErrorMessage = "Name for Female Winner #2 is required.")]
        [StringLength(75, ErrorMessage = "Name must not exceed 75 characters.")]
        public string FemaleWinner2Name { get; set; }
        [Required(ErrorMessage = "Time for Female Winner #2 is required.")]
        [DisplayFormat(DataFormatString = "{0:mm\\:ss}")]
        [DataType(DataType.Time)]
        public TimeSpan FemaleWinner2Time { get; set; }
        [Required(ErrorMessage = "Name for Female Winner #3 is required.")]
        [StringLength(75, ErrorMessage = "Name must not exceed 75 characters.")]
        public string FemaleWinner3Name { get; set; }
        [Required(ErrorMessage = "Time for Female Winner #3 is required.")]
        [DisplayFormat(DataFormatString = "{0:mm\\:ss}")]
        [DataType(DataType.Time)]
        public TimeSpan FemaleWinner3Time { get; set; }
        [DataType(DataType.Url)]
        [StringLength(200, ErrorMessage = "Name must not exceed 200 characters.")]
        public string ExternalResultsUrl { get; set; }
        [DataType(DataType.Url)]
        [StringLength(200, ErrorMessage = "Name must not exceed 200 characters.")]
        public string RacePhotosUrl { get; set; }
    }
    public class RaceVideoMetadata
    {
        public int RaceVideoId { get; set; }
        [Required(ErrorMessage = "Race Year is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        [DataType(DataType.Date)]
        public DateTime RaceYear { get; set; }
        [StringLength(50, ErrorMessage = "Video Title must not exceed 50 characters.")]
        public string VideoTitle { get; set; }
        [DataType(DataType.Url)]
        [Required(ErrorMessage = "Video Url is required.")]
        [StringLength(75, ErrorMessage = "Name must not exceed 75 characters.")]
        public string VideoUrl { get; set; }
    }
    public class SponsorMetadata
    {
        public int SponsorId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(150, ErrorMessage = "Name must not exceed 150 characters.")]
        public string SponsorName { get; set; }
        [StringLength(200, ErrorMessage = "URL must not exceed 200 characters.")]
        [DataType(DataType.Url)]
        public string SponsorUrl { get; set; }
        [StringLength(75, ErrorMessage = "Logo name must not exceed 75 characters.")]
        [DataType(DataType.ImageUrl)]
        public string SponsorLogo { get; set; }
        public int SponsorTypeId { get; set; }
        [StringLength(100, ErrorMessage = "Sponsor Video URL must not exceed 100 characters.")]
        [DataType(DataType.Url)]
        public string SponsorVideoUrl { get; set; }
    }
    public class SponsorTypeMetadata
    {
        public int SponsorTypeId { get; set; }
        [Required(ErrorMessage = "Sponsor Type Name is required.")]
        [StringLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string SponsorTypeName { get; set; }
        [Required(ErrorMessage = "Cost is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid integer.")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public int Cost { get; set; }
        [Required(ErrorMessage = "Number Offered is required.")]
        [Range(0, short.MaxValue, ErrorMessage = "Please enter a valid integer.")]
        
        public short NbrOffered { get; set; }
    }
    public class SponsorTypeBenefitMetadata
    {
        public int STBID { get; set; }
        [Required(ErrorMessage = "Sponsor Type is required.")]
        public int SponsorTypeId { get; set; }
        [Required(ErrorMessage = "Benefit is required.")]
        public int BenefitId { get; set; }
    }
}
