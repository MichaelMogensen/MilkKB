namespace DRDownload.Common.Types.BroadcastTypes
{
    public class Broadcast
    {
        public EMediaType MediaType { get; set; }
        public string? EntityId { get; set; }
        public string? Title { get; set; }
        public DateTime SendDate { get; set; }
        public int DurationMin { get; set; }
        public TimeSpan Duration => TimeSpan.FromMinutes(DurationMin);
        public string? Channel { get; set; }
        public string? ExtraInfo { get; set; }

        public override string ToString() =>
            $"{MediaType}, {EntityId}, {Title}, {SendDate}, {DurationMin}, {Channel}, {ExtraInfo}";
    }
}

// New props: Id as Guid, Episode, Genre.