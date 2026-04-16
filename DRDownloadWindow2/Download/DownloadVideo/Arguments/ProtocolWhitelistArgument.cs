using FFMpegCore.Arguments;

namespace DRDownload.Common.DownloadVideo.Arguments
{
    public class ProtocolWhitelistArgument : IArgument
    {
        public string Text => "-protocol_whitelist file,http,https,tcp,tls,crypto";
    }
}

