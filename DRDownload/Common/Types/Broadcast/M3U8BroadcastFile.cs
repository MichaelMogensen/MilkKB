namespace DRDownload.Common.Types.Broadcast
{
    /// <summary>
    /// m2u8 playlist is used for video.
    /// </summary>
    public class M3U8BroadcastFile : BroadcastFile
    {
        public M3U8BroadcastFile(string basePath, BroadcastMetadata bmd) : base(basePath, "m3u", bmd)
        {
        }
    }
}

