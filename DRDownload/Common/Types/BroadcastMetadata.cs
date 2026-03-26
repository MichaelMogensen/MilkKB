namespace DRDownload.Common.Types
{
    public struct BroadcastMetadata
    {
        public string Title { get; set; }
        public DateTime SendDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string Channel { get; set; }

        public string? ExtraInfo { get; set; }

        public BroadcastMetadata(string title, DateTime sendDate, TimeSpan duration, string channel, string? extraInfo = null)
        {
            Title = title;
            SendDate = sendDate;
            Duration = duration;
            Channel = channel;
            ExtraInfo = extraInfo;
        }
    }
}

