using DRDownload.Common.Types.BroadcastTypes;

namespace DRDownload.Common.Types.BroadcastFiles
{
    public abstract class BroadcastFile
    {
        public string File { get; private set; } = string.Empty;

        public BroadcastFile(string basePath, string ext, Broadcast broadcast)
        {
            CreateFilename(basePath, ext, broadcast);
        }

        private void CreateFilename(string basePath, string ext, Broadcast broadcast)
        {
            var title = broadcast.Title;
            var sendDate = Util.ToDanishDate(broadcast.SendDate);
            var duration = Util.ToDanishDuration(broadcast.SendDate, broadcast.Duration);
            var ch = broadcast.Channel;
            var extraInfo = string.IsNullOrEmpty(broadcast.ExtraInfo) ? null : $" ({broadcast.ExtraInfo})";

            var filetitle = $"{title}, {sendDate}, {duration} på {ch}{extraInfo}.{ext}";

            File = Path.Combine(basePath, filetitle);
            File = File.ToLower();
        }
    }
}

