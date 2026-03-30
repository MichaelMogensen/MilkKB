using DRDownload.Common.DownloadFile;
using DRDownload.Common.DownloadVideo;
using DRDownload.Common.Types.Broadcast;
using DRDownload.Common.Types.RestAPI;

namespace DRDownload.Video
{
    public class DRVideo
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public DRVideo()
        {
        }

        /// <summary>
        /// N downloads.
        /// </summary>
        /// <param name="cts"></param>
        /// <returns></returns>
        public async Task DownloadVideoBroadcastsAsync(CancellationToken cts)
        {
            await DownloadVideoBroadcastAsync(
                cts,
                "0_tsmswb48",
                @"C:\Temp\Video\Dansk Naturgas",
                "Dansk Naturgas kavalkade",
                new DateTime(1987, 12, 31, 23, 10, 0),
                TimeSpan.FromMinutes(45),
                "DR1",
                "What did ju have in jur tæjsk today");
        }
        public async Task DownloadVideoBroadcastsAsync()
        {
            var cts = new CancellationTokenSource();
            await DownloadVideoBroadcastsAsync(cts.Token);
        }

        /// <summary>
        /// 1 download.
        /// </summary>
        /// <param name="cts">Cancel</param>
        /// <param name="entityId">From behind DR site</param>
        /// <param name="basePath">On local</param>
        /// <param name="title"></param>
        /// <param name="sendDate">Metadata from DR site</param>
        /// <param name="duration">Metadata from DR site</param>
        /// <param name="channel">Metadata from DR site</param>
        /// <param name="extraInfo">Metadata from DR site</param>
        /// <returns></returns>
        private async Task DownloadVideoBroadcastAsync(
            CancellationToken cts,
            string entityId,
            string basePath,
            string title,
            DateTime sendDate,
            TimeSpan duration,
            string channel,
            string? extraInfo = null)
        {
            // Prepare m3u8 file download.
            var url = new RestAPIUrlVideo(entityId).Url;
            var bmd = new BroadcastMetadata(
                    title,
                    sendDate,
                    duration,
                    channel,
                    extraInfo);
            var m3u8File = new M3U8BroadcastFile(
                basePath,
                bmd).File;
            if (File.Exists(m3u8File))
            {
                File.Delete(m3u8File);
            }

            // Download m3u8 playlist and video.
            var m3uDownloader = new DownloadFileStream(url, m3u8File);
            var mp4Downloader = new DownloadVideoStream(m3u8File);

            Console.WriteLine($"Downloading ...");
            Console.WriteLine();
            Console.WriteLine($"Playlist: {mp4Downloader.InputFile}");
            Console.WriteLine($"Video: {mp4Downloader.OutputFile}");
            Console.WriteLine($"Log: {mp4Downloader.LogFile}");
            Console.WriteLine();
            Console.WriteLine($"Please wait ...");
            Console.WriteLine();

            await m3uDownloader.StartAsync();
            await mp4Downloader.StartAsync(cts);

            Console.WriteLine($"DONE");
        }

    }
}

