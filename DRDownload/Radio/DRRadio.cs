using DRDownload.Common;
using DRDownload.Common.Types;

namespace DRDownload.Radio
{
    public class DRRadio : DRRadioAndVideoBase
    {
        public DRRadio()
        {
        }

        public async Task DownloadRadioBroadcastsAsync()
        {
            var url = new DRUrlRadio("0_xnpa9rhu").Url;
            var file = new LocalRadioFilename(
                @"c:\temp",
                new BroadcastMetadata(
                    "Men Kærligheden", 
                    new DateTime(1989, 10, 2, 14, 0, 0), 
                    TimeSpan.FromHours(1), 
                    "P1", 
                    "Montage om Alma og Gustav Mahler")).File;
            await DownloadStreamAsync(url, file);
        }
    }
}

