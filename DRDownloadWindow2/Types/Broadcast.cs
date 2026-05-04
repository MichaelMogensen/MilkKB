using DRDownloadWindow2.OneValueSettingFile;

namespace DRDownloadWindow2.Types
{
    public class Broadcast
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? SendDate { get; set; }
        public int? DurationMin { get; set; }
        public TimeSpan? Duration => DurationMin == null ? null : TimeSpan.FromMinutes(DurationMin.Value);
        public string? Episode { get; set; }
        public string? Channel { get; set; }
        public string? Genre { get; set; }

        public string? UniqueId { get; set; }
        public string? EntryId { get; set; }
        public EMediaType? MediaType { get; set; }
        public string? Url { get; set; }
        public string? DownloadFolder { get; set; }
        public string? Mp3File { get; set; }
        public string? M3uFile { get; set; }
        public string? Mp4File { get; set; }
        public string? LogFile { get; set; }

        public bool GenerateLogFile { get; set; } = new LoadOneValueFromASettingFile(nameof(GenerateLogFile)).ValueOrDefault(false);
        public bool DeleteM3uFileAfterDownload { get; set; } = new LoadOneValueFromASettingFile(nameof(DeleteM3uFileAfterDownload)).ValueOrDefault(true);
    }
}

