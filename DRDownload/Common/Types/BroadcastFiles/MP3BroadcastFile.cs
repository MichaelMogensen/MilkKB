using DRDownload.Common.Types.BroadcastTypes;

namespace DRDownload.Common.Types.BroadcastFiles
{
    /// <summary>
    /// mp3 playlist is used for radio.
    /// </summary>
    public class MP3BroadcastFile : BroadcastFileBase
    {
        public MP3BroadcastFile(string basePath, Broadcast broadcast) : base(basePath, "mp3", broadcast)
        {
        }
    }
}

