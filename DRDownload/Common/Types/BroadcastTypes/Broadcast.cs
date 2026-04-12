namespace DRDownload.Common.Types.BroadcastTypes
{
    public class Broadcast
    {
        public string? UniqueId { get; private set; } = Util.GenerateRandomGuid();
        public EMediaType MediaType { get; set; }
        public string? EntityId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime SendDate { get; set; }
        public int DurationMin { get; set; }
        public TimeSpan Duration => TimeSpan.FromMinutes(DurationMin);
        public string? Channel { get; set; }
        public string Episode { get; set; }
        public string Genre { get; set; }

        public override string ToString() =>
             "new Broadcast" + Environment.NewLine +
             "{" + Environment.NewLine +
            $"    UniqueId = {UniqueId}, " + Environment.NewLine +
            $"    MediaType = {MediaType}, " + Environment.NewLine +
            $"    EntityId = {EntityId}, " + Environment.NewLine +
            $"    Title = {Title}, " + Environment.NewLine +
            $"    Description = {Description}, " + Environment.NewLine +
            $"    SendDate = {SendDate}, " + Environment.NewLine +
            $"    DurationMin = {DurationMin}, " + Environment.NewLine +
            $"    Channel = {Channel}, " + Environment.NewLine +
            $"    Episode = {Episode}, " + Environment.NewLine +
            $"    Genre = {Genre}" + Environment.NewLine +
             "}";

        /// <summary>
        /// Ctor.
        /// </summary>
        public Broadcast()
        {
            
        }
    }
}

// New props: Id as Guid, Episode, Genre.