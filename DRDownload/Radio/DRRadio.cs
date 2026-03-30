using DRDownload.Common.DownloadFile;
using DRDownload.Common.Types.Broadcast;
using DRDownload.Common.Types.RestAPI;

namespace DRDownload.Radio
{
    public class DRRadio
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public DRRadio()
        {
        }

        /// <summary>
        /// N downloads. Look for entryId on KB site.
        /// </summary>
        public async Task DownloadRadioBroadcastsAsync()
        {
            await DownloadRadioBroadcastAsync(
                "0_fhu1v7fr",
                @"c:\temp\Radio",
                "Radioklassikeren Herning",
                    new DateTime(2007, 10, 6, 7, 3, 0),
                    TimeSpan.FromMinutes(57),
                    "P1",
                    "Byen hvor trådene knyttes. Det var sloganet for tekstil- og beklædningsbyen Herning, der har været præget af flid, nøjsomhed, stræbsomhed og Gudsfrygt");

        }

        /// <summary>
        /// 1 download.
        /// </summary>
        /// <param name="entityId">From behind DR site</param>
        /// <param name="basePath">On local</param>
        /// <param name="title"></param>
        /// <param name="sendDate">Metadata from DR site</param>
        /// <param name="duration">Metadata from DR site</param>
        /// <param name="channel">Metadata from DR site</param>
        /// <param name="extraInfo">Metadata from DR site</param>
        /// <returns></returns>
        public async Task DownloadRadioBroadcastAsync(
            string entityId,
            string basePath,
            string title,
            DateTime sendDate,
            TimeSpan duration,
            string channel,
            string? extraInfo = null)
        {
            var mp3Downloader = new DownloadFileStream(
                new RestAPIUrlRadio(entityId).Url,
                new MP3BroadcastFile(
                    basePath,
                    new BroadcastMetadata(
                        title,
                        sendDate,
                        duration,
                        channel,
                        extraInfo)).File);

            Console.WriteLine($"Downloading ...");
            Console.WriteLine();
            Console.WriteLine($"Radio: {mp3Downloader.File}");
            Console.WriteLine();
            Console.WriteLine($"Please wait ...");
            Console.WriteLine();

            await mp3Downloader.StartAsync();
            
            Console.WriteLine($"DONE");
        }

    }
}

