using DRDownloadWindow2.Models;

namespace DRDownloadWindow2.ViewModels
{
    public interface IBroadcastViewModel
    {
        public BroadcastModel Model { get; set; }

        // User prop's.
        public string? Title { get; set; }
        public string? SendDateAndDuration { get; set; }
        public string? Description { get; set; }
        public string? Episode { get; set; }
        public string? Channel { get; set; }
        public string? Genre { get; set; }

        // Technical prop's.
        public string? UniqueId { get; set; }
        public string? EntryId { get; set; }
        public string? MediaType { get; set; }
        public string? Url { get; set; }
        public string? DownloadFolder { get; set; }
        public string? Mp3File { get; set; }
        public string? M3uFile { get; set; }
        public string? Mp4File { get; set; }
        public string? LogFile { get; set; }

        public string? StatusBarText { get; set; }
        public int? ProgressBarPercent { get; set; }
    }
}

