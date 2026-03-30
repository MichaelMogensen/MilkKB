namespace DRDownload.Common.Types.Broadcast
{
    /// <summary>
    /// mp3 playlist is used for radio.
    /// </summary>
    public class MP3BroadcastFile : BroadcastFile
    {
        public MP3BroadcastFile(string basePath, BroadcastMetadata bmd) : base(basePath, "mp3", bmd)
        {
        }
    }
}

