using System;
using System.Collections.Generic;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class MessageFromTom
    {
        public int MessageId { get; set; }
        public string MessageContent { get; set; } = null!;
        public string? Photo1 { get; set; }
        public string? Photo2 { get; set; }
        public string? Photo3 { get; set; }
        public string? Photo4 { get; set; }
    }
}
