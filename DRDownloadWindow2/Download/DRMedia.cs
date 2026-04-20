using DRDownload.Common.DownloadFile;
using DRDownload.Common.DownloadVideo;
using DRDownloadWindow2.Download.KLTRRestAPI;
using DRDownloadWindow2.Types;
using System.Diagnostics;
using File = System.IO.File;

namespace DRDownloadWindow2.Download
{
    /// <summary>
    /// Top class to download broadcasts from https://www.kb.dk/find-materiale/dr-arkivet/ by entryId.
    /// </summary>
    public class DRMedia
    {
        private Broadcast Broadcast { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public DRMedia(Broadcast broadcast)
        {
            Broadcast = broadcast;
        }

        /// <summary>
        /// Start radio or tv download.
        /// </summary>
        /// <param name="cts"></param>
        /// <returns></returns>
        public async Task StartDownloadAsync(CancellationToken cts)
        {
            if (Broadcast.MediaType == null || Broadcast.MediaType == EMediaType.nomedia)
            {
                throw new ArgumentNullException($"Cannot start download since media type is unknown at start. Has to be radio or tv");
            }

            if (Broadcast.MediaType == EMediaType.radio)
                await StartRadioDownloadAsync();
            else if (Broadcast.MediaType == EMediaType.tv)
                await StartTvDownloadAsync(cts);
        }

        /// <summary>
        /// Start radio download.
        /// </summary>
        /// <returns></returns>
        private async Task StartRadioDownloadAsync()
        {
            if (string.IsNullOrEmpty(Broadcast.Mp3File))
            {
                throw new ArgumentNullException($"Cannot start radio download since output file is unknown at start");
            }

            var restUrl = new KLTRRestAPIUrlRadio(Broadcast.EntryId).Url;
            var mp3Downloader = new DownloadFileStream(restUrl, Broadcast.Mp3File);

            // If output file already exists we abord.
            if (File.Exists(mp3Downloader.OutputFile))
            {
                return;
            }

            // Begin download of mp3 file.
            await DownloadAsync(mp3Downloader.StartAsync);
        }

        /// <summary>
        /// Start tv download.
        /// </summary>
        /// <param name="cts"></param>
        /// <returns></returns>
        private async Task StartTvDownloadAsync(CancellationToken cts)
        {
            if (string.IsNullOrEmpty(Broadcast.M3uFile))
            {
                throw new ArgumentNullException($"Cannot start tv download since input file is unknown at start");
            }
            if (string.IsNullOrEmpty(Broadcast.Mp4File))
            {
                throw new ArgumentNullException($"Cannot start tv download since output file is unknown at start");
            }
            if (string.IsNullOrEmpty(Broadcast.LogFile))
            {
                throw new ArgumentNullException($"Cannot start tv download since log file is unknown at start");
            }
            if (Broadcast.Duration == null || Broadcast.Duration.Value == default)
            {
                throw new ArgumentNullException($"Cannot start tv download since expected total duration of video is unknown at start");
            }

            // Prepare m3u8 file download.
            var restUrl = new KLTRRestAPIUrlVideo(Broadcast.EntryId).Url;

            // Download m3u8 playlist and video.
            var m3uDownloader = new DownloadFileStream(
                restUrl,
                Broadcast.M3uFile);
            var mp4Downloader = new DownloadVideoStream(
                Broadcast.M3uFile,
                Broadcast.Mp4File,
                Broadcast.LogFile,
                Broadcast.Duration);

            // If output file already exists we abord.
            if (File.Exists(Broadcast.Mp4File))
            {
                return;
            }

            await DownloadAsync(async () =>
            {
                if (!File.Exists(Broadcast.M3uFile))
                {
                    await m3uDownloader.StartAsync();
                }
                await mp4Downloader.StartAsync(cts);
            });
        }

        /// <summary>
        /// General timed download for both radio and tv.
        /// </summary>
        /// <param name="DownloadAsync"></param>
        /// <returns></returns>
        private async Task DownloadAsync(Func<Task> DownloadAsync)
        {
            var watch = new Stopwatch();

            watch.Start();
            await DownloadAsync();
            watch.Stop();
        }

    }
}

