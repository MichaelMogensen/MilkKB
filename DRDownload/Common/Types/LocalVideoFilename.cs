namespace DRDownload.Common.Types
{
    public class LocalVideoFilename : LocalFilename
    {
        public LocalVideoFilename(string basePath, BroadcastMetadata bmd) : base(basePath, "m3u", bmd)
        {
        }
    }
}

