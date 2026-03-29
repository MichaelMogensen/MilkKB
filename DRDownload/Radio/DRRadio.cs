using DRDownload.Common.Types;
using DRDownload.Common.Types.Broadcast;
using DRDownload.Common.Types.RestAPI;

namespace DRDownload.Radio
{
    public class DRRadio
    {
        public DRRadio()
        {
        }

        public async Task DownloadRadioBroadcastsAsync()
        {
            var url = new RestAPIUrlRadio("0_xnpa9rhu").Url;
            var file = new MP3BroadcastFile(
                @"c:\temp",
                new BroadcastMetadata(
                    "Men Kærligheden",
                    new DateTime(1989, 10, 2, 14, 0, 0),
                    TimeSpan.FromHours(1),
                    "P1",
                    "Montage om Alma og Gustav Mahler")).File;
            await Download.DownloadStreamAsync(url, file);
        }
    }
}

