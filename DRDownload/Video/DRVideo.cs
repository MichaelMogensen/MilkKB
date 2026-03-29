using DRDownload.Common;
using DRDownload.Common.FFMPEG;
using DRDownload.Common.Types;
using DRDownload.Common.Types.Broadcast;
using DRDownload.Common.Types.RestAPI;

namespace DRDownload.Video
{
    public class DRVideo
    {
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

        /// <summary>
        /// 1 download.
        /// </summary>
        /// <param name="cts">Cancel</param>
        /// <param name="entityId">From DR site</param>
        /// <param name="basePath">On local</param>
        /// <param name="title"></param>
        /// <param name="sendDate">Metadata from DK site</param>
        /// <param name="duration">Metadata from DK site</param>
        /// <param name="channel">Metadata from DK site</param>
        /// <param name="extraInfo">Metadata from DK site</param>
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
            // Download m3u8 file.
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

            var ffmpegVideoDownloader = new FFMPEGVideoDownloader(m3u8File);


            // Download m3u8 and video.

            Console.WriteLine($"Downloading m3u8 playlist: {ffmpegVideoDownloader.InputFile}...");
            await Download.DownloadStreamAsync(url, ffmpegVideoDownloader.InputFile);
            Console.WriteLine($"Downloading video: {ffmpegVideoDownloader.OutputFile}...");
            await new FFMPEGVideoDownloader(m3u8File).DownloadVideoAsync(cts);
            Console.WriteLine($"See log: {ffmpegVideoDownloader.LogFile}");
            Console.WriteLine($"DONE");
        }

    }
}

