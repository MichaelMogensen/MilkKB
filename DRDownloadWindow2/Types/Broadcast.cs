using DRDownloadWindow2.Utilities;

namespace DRDownloadWindow2.Types
{
    public class Broadcast
    {
        public string? UniqueId { get; set; }
        public EMediaType? MediaType { get; set; }
        public string? EntryId { get; set; }
        public string? Channel { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? SendDate { get; set; }
        public int? DurationMin { get; set; }
        public TimeSpan? Duration => DurationMin == null ? null : TimeSpan.FromMinutes(DurationMin.Value);
        public string? Episode { get; set; }
        public string? Genre { get; set; }
        public string? Url { get; set; }

        public string? DownloadFolder { get; set; }
        public string? Mp3File { get; set; }
        public string? M3uFile { get; set; }
        public string? Mp4File { get; set; }
        public string? LogFile { get; set; }

        public override string ToString() =>
            $"{nameof(UniqueId)} = {Util.OrNil(UniqueId)}|" +
            $"{nameof(MediaType)} = {Util.OrNil(MediaType)}|" +
            $"{nameof(EntryId)} = {Util.OrNil(EntryId)}|" +
            $"{nameof(Channel)} = {Util.OrNil(Channel)}|" +
            $"{nameof(Title)} = {Util.OrNil(Title)}|" +
            $"{nameof(Description)} = {Util.OrNil(Description)}|" +
            $"{nameof(SendDate)} = {Util.OrNil(SendDate)}|" +
            $"{nameof(DurationMin)} = {Util.OrNil(DurationMin)}|" +
            $"{nameof(Episode)} = {Util.OrNil(Episode)}|" +
            $"{nameof(Genre)} = {Util.OrNil(Genre)}|" +
            $"{nameof(Url)} = {Util.OrNil(Url)}|" +
            $"{nameof(DownloadFolder)} = {Util.OrNil(DownloadFolder)}|" +
            $"{nameof(Mp3File)} = {Util.OrNil(Mp3File)}|" +
            $"{nameof(M3uFile)} = {Util.OrNil(M3uFile)}|" +
            $"{nameof(Mp4File)} = {Util.OrNil(Mp4File)}|" +
            $"{nameof(LogFile)} = {Util.OrNil(LogFile)}";
    }
}

