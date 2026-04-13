namespace DRDownload.Common.Types.BroadcastTypes
{
    public class Broadcast
    {
        public string? UniqueId { get; set; }
        public string? EntityId { get; set; }
        public string? Channel { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime SendDate { get; set; }
        public int DurationMin { get; set; }
        public TimeSpan Duration => TimeSpan.FromMinutes(DurationMin);
        public string? Episode { get; set; }
        public string? Genre { get; set; }
        public EMediaType MediaType { get; set; }

        public override string ToString() => 
            $"UniqueId = {Util.OrNull(UniqueId)}, " +
            $"EntityId = {Util.OrNull(EntityId)}, " +
            $"Channel = {Util.OrNull(Channel)}, " +
            $"Title = {Util.OrNull(Title)}, " +
            $"Description = {Util.OrNull(Description)}, " +
            $"SendDate = {Util.OrNull(SendDate)}, " +
            $"DurationMin = {Util.OrNull(DurationMin)}, " +
            $"Episode = {Util.OrNull(Episode)}, " +
            $"Genre = {Util.OrNull(Genre)}, " +
            $"MediaType = {Util.OrNull(MediaType)}";
    }
}

