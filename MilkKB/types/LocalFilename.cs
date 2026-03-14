using MilkKB.util;

namespace MilkKB.types
{
    public class LocalFilename
    {
        public string Result { get; private set; }

        public LocalFilename(string basePath, string ext, BroadcastMetadata bmd)
        {
            CreateFilename(basePath, ext, bmd);
        }

        private void CreateFilename(string basePath, string ext, BroadcastMetadata bmd)
        {
            var title = bmd.Title;
            var sendDate = Util.ToDanishDate(bmd.SendDate);
            var duration = Util.ToDanishDuration(bmd.SendDate, bmd.Duration);
            var ch = bmd.Channel;

            var filetitle = $"{title}, {sendDate}, {duration} på {ch}.{ext}";

            Result = Path.Combine(basePath, filetitle);
        }
    }
}

