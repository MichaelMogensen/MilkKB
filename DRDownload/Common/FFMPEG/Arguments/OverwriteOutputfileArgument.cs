using FFMpegCore.Arguments;

namespace DRDownload.Common.FFMPEG.Arguments
{
    public class OverwriteOutputfileArgument : IArgument
    {
        public string Text => "-y";
    }
}

