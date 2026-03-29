namespace DRDownload.Common.Types.Broadcast
{
    public abstract class BroadcastFile
    {
        public string File { get; private set; } = string.Empty;

        public BroadcastFile(string basePath, string ext, BroadcastMetadata bmd)
        {
            CreateFilename(basePath, ext, bmd);
        }

        private void CreateFilename(string basePath, string ext, BroadcastMetadata bmd)
        {
            var title = bmd.Title;
            var sendDate = Util.ToDanishDate(bmd.SendDate);
            var duration = Util.ToDanishDuration(bmd.SendDate, bmd.Duration);
            var ch = bmd.Channel;
            var extraInfo = string.IsNullOrEmpty(bmd.ExtraInfo) ? null : $" ({bmd.ExtraInfo})";

            var filetitle = $"{title}, {sendDate}, {duration} på {ch}{extraInfo}.{ext}";

            File = Path.Combine(basePath, filetitle);
        }
    }
}

