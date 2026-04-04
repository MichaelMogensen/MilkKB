using DRDownload.Common.Types.BroadcastTypes;

namespace DRDownload.Common.Types.BroadcastFiles
{
    /// <summary>
    /// m3u8 playlist is used for video.
    /// </summary>
    public class M3U8BroadcastFile : BroadcastFile
    {
        public M3U8BroadcastFile(string basePath, Broadcast broadcast) : base(basePath, "m3u", broadcast)
        {
        }
    }
}

