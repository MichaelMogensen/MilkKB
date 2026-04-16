using DRDownloadWindow2.Types;

namespace DRDownloadWindow2.DRBroadcast.DRBroadcastFile
{
    /// <summary>
    /// mp3 playlist is used for radio.
    /// </summary>
    public class DRMP3BroadcastFile : DRBroadcastFileBase
    {
        public DRMP3BroadcastFile(Broadcast broadcast) : base("mp3", broadcast)
        {
        }
    }
}

