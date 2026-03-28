using DRDownload.Common;
using DRDownload.Common.Types;
using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Enums;
using System.Net.Sockets;
using System.Resources;

namespace DRDownload.Video
{
    public class DRVideo : DRRadioAndVideoBase
    {
        public DRVideo()
        {
        }

        public async Task DownloadVideoBroadcastsAsync()
        {
            //var url = new DRUrlVideo("0_ela5z5u2").Url;
            //var file = new LocalVideoFilename(
            //    @"c:\temp",
            //    new BroadcastMetadata(
            //        "Lørdagshjørnet - Otto Leisner",
            //        new DateTime(2014, 4, 19, 17, 58, 0),
            //        new TimeSpan(0, 49, 7),
            //        "DR-K",
            //        "Aftenens gæst i Lørdagshjørnet er Otto Leisner")).File;
            //await DownloadStreamAsync(url, file);

            var url = new DRUrlVideo("0_tsmswb48").Url;
            var file = new LocalVideoFilename(
                @"c:\temp",
                new BroadcastMetadata(
                    "Dansk Naturgas kavalkade",
                    new DateTime(1987, 12, 31, 23, 10, 0),
                    TimeSpan.FromMinutes(45),
                    "DR1",
                    "What did ju have in jur tæjsk today")).File;
            await DownloadStreamAsync(url, file);
        }

        class ProtocolWhitelistArgument : IArgument
        {
            public string Text => "-protocol_whitelist file,http,https,tcp,tls,crypto";
        }

        public async Task DownloadAsync()
        {
            await FFMpegArguments
                .FromFileInput(@"C:\Temp\Otto.m3u", true, op => op
                    .WithArgument(new ProtocolWhitelistArgument())
                    )
                .OutputToFile(@"C:\Temp\Otto3.mp4", true, op => op
                    .WithFastStart())
                .ProcessAsynchronously();
        }

    }
}

