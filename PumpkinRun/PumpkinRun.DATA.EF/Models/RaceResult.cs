using System;
using System.Collections.Generic;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class RaceResult
    {
        public int RaceResultId { get; set; }
        public DateTime RaceYear { get; set; }
        public string MaleWinner1Name { get; set; } = null!;
        public TimeSpan MaleWinner1Time { get; set; }
        public string MaleWinner2Name { get; set; } = null!;
        public TimeSpan MaleWinner2Time { get; set; }
        public string MaleWinner3Name { get; set; } = null!;
        public TimeSpan MaleWinner3Time { get; set; }
        public string FemaleWinner1Name { get; set; } = null!;
        public TimeSpan FemaleWinner1Time { get; set; }
        public string FemalWinner2Name { get; set; } = null!;
        public TimeSpan FemaleWinner2Time { get; set; }
        public string FemaleWinner3Name { get; set; } = null!;
        public TimeSpan FemaleWinner3Time { get; set; }
        public string? ExternalResultsUrl { get; set; }
        public string? RacePhotosUrl { get; set; }
    }
}
