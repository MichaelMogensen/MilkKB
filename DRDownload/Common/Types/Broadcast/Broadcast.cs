using System;
using System.Collections.Generic;
using System.Text;

namespace DRDownload.Common.Types.Broadcast
{
    public class Broadcast
    {
        public string? EntityId { get; set; }
        public string? Title { get; set; }
        public DateTime SendDate { get; set; }
        public int DurationMin { get; set; }
        public string? Channel { get; set; }
        public string? ExtraInfo { get; set; }
    }
}

