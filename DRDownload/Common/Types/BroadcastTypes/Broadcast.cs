namespace DRDownload.Common.Types.BroadcastTypes
{
    public class Broadcast
    {
        public string? UniqueId { get; set; }
        public EMediaType MediaType { get; set; }
        public string? EntityId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime SendDate { get; set; }
        public int DurationMin { get; set; }
        public TimeSpan Duration => TimeSpan.FromMinutes(DurationMin);
        public string? Channel { get; set; }
        public string? Episode { get; set; }
        public string? Genre { get; set; }

        public override string ToString() => "TODO";
    }
}

