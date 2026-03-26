namespace DRDownload.Common.Types
{
    public class LocalRadioFilename : LocalFilename
    {
        public LocalRadioFilename(string basePath, BroadcastMetadata bmd) : base(basePath, "mp3", bmd)
        {
        }
    }
}

