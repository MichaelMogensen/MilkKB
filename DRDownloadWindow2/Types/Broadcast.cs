using DRDownloadWindow2.Utilities;

namespace DRDownloadWindow2.Types
{
    public class Broadcast
    {
        public string? UniqueId { get; set; }
        public EMediaType MediaType { get; set; }
        public string? EntryId { get; set; }
        public string? Channel { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime SendDate { get; set; }
        public int DurationMin { get; set; }
        public TimeSpan Duration => TimeSpan.FromMinutes(DurationMin);
        public string? Episode { get; set; }
        public string? Genre { get; set; }

        public override string ToString() =>
            $"{nameof(UniqueId)} = {Util.OrNull(UniqueId)}|" +
            $"{nameof(MediaType)} = {Util.OrNull(MediaType)}|" +
            $"{nameof(EntryId)} = {Util.OrNull(EntryId)}|" +
            $"{nameof(Channel)} = {Util.OrNull(Channel)}|" +
            $"{nameof(Title)} = {Util.OrNull(Title)}|" +
            $"{nameof(Description)} = {Util.OrNull(Description)}|" +
            $"{nameof(SendDate)} = {Util.OrNull(SendDate)}|" +
            $"{nameof(DurationMin)} = {Util.OrNull(DurationMin)}|" +
            $"{nameof(Episode)} = {Util.OrNull(Episode)}|" +
            $"{nameof(Genre)} = {Util.OrNull(Genre)}";
    }
}

