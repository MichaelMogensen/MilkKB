using DRDownload.Common;
using DRDownload.Common.Types;

namespace DRDownload.Video
{
    public class DRVideo : DRRadioAndVideoBase
    {
        public DRVideo()
        {
        }

        public async Task DownloadVideoBroadcastsAsync()
        {
            var url = new DRUrlVideo("0_ela5z5u2").Url;
            var file = new LocalVideoFilename(
                @"c:\temp",
                new BroadcastMetadata(
                    "Lørdagshjørnet - Otto Leisner",
                    new DateTime(2014, 4, 19, 17, 58, 0),
                    new TimeSpan(0, 49, 7),
                    "DR-K",
                    "Aftenens gæst i Lørdagshjørnet er Otto Leisner")).File;
            await DownloadStreamAsync(url, file);
        }
    }
}

