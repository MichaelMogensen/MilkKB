using DRDownloadWindow.Types;

namespace DRDownloadWindow.DRBroadcast.DRBroadcastFile
{
    /// <summary>
    /// m3u8 playlist is used for video.
    /// </summary>
    public class DRM3U8BroadcastFile : DRBroadcastFileBase
    {
        public DRM3U8BroadcastFile(Broadcast broadcast) : base("m3u", broadcast)
        {
        }
    }
}

