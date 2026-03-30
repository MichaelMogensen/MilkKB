using FFMpegCore.Arguments;

namespace DRDownload.Common.DownloadVideo.Arguments
{
    public class OverwriteOutputfileArgument : IArgument
    {
        public string Text => "-y";
    }
}

