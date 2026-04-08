using DRDownload.Common.Types.BroadcastTypes;

namespace DRDownload.Common.Types.BroadcastFiles
{
    public abstract class BroadcastFileBase
    {
        public string OutputFile { get; private set; } = string.Empty;

        public BroadcastFileBase(string basePath, string ext, Broadcast broadcast)
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

            OutputFile = Path.Combine(basePath, filetitle);
            OutputFile = OutputFile.ToLower();
        }
    }
}

