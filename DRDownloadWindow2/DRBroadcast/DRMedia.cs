using DRDownload.Common.DownloadFile;
using DRDownload.Common.DownloadVideo;
using DRDownloadWindow.Download.KLTRRestAPI;
using DRDownloadLib.Types;
using DRDownloadWindow.Utilities;
using System.Diagnostics;
using File = System.IO.File;

namespace DRDownloadWindow.DRBroadcast
{
    /// <summary>
    /// Top class to download broadcasts from https://www.kb.dk/find-materiale/dr-arkivet/ by entryId.
    /// </summary>
    public class DRMedia
    {
        private Broadcast Broadcast { get; set; }

        public StatusAndProgressHandler StatusAndProgressHandler { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="broadcast"></param>
        /// <param name="statusAndProgressHandler"></param>
        public DRMedia(
            Broadcast broadcast,
            StatusAndProgressHandler statusAndProgressHandler)
        {
            Broadcast = broadcast;
            StatusAndProgressHandler = statusAndProgressHandler;
        }

        /// <summary>
        /// Start radio or tv download.
        /// </summary>
        /// <param name="cts"></param>
        /// <returns></returns>
        public async Task StartDownloadAsync(CancellationToken cts)
        {
            if (Broadcast.MediaType == EMediaType.radio)
            {
                await StartRadioDownloadAsync();
            }
            else if (Broadcast.MediaType == EMediaType.tv)
            {
                await StartTvDownloadAsync(cts);
            }
            else
            {
                StatusAndProgressHandler.UpdateStatus("Medie type skal være enten radio eller tv", EWarningLevel.error);
            }
        }

        /// <summary>
        /// Start radio download.
        /// </summary>
        /// <returns></returns>
        private async Task StartRadioDownloadAsync()
        {
            if (string.IsNullOrEmpty(Broadcast.Mp3File))
            {
                StatusAndProgressHandler.UpdateStatus("Kan ikke starte radio download med manglende mp3 fil", EWarningLevel.error);
                return;
            }

            var restUrl = new KLTRRestAPIUrlRadio(Broadcast.EntryId).Url;
            var mp3Downloader = new DownloadFileStream(
                restUrl,
                Broadcast.Mp3File,
                Broadcast.GenerateLogFile ? Broadcast.LogFile : null,
                StatusAndProgressHandler);

            // If output file already exists we abort.
            if (File.Exists(mp3Downloader.OutputFile))
            {
                StatusAndProgressHandler.UpdateStatus($"Filen {mp3Downloader.OutputFile} findes allerede. Slet den hvis du vil downloade igen", EWarningLevel.error);
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
                StatusAndProgressHandler.UpdateStatus("Kan ikke starte tv download uden en m3u8 playlist-fil", EWarningLevel.error);
                return;
            }
            if (string.IsNullOrEmpty(Broadcast.Mp4File))
            {
                StatusAndProgressHandler.UpdateStatus("Kan ikke starte tv download med manglende mp4 fil", EWarningLevel.error);
                return;
            }

            // Ensure button is dimmed early on.
            StatusAndProgressHandler.UpdateProgress(1);

            // Prepare m3u8 file download.
            var restUrl = new KLTRRestAPIUrlVideo(Broadcast.EntryId).Url;

            // First download m3u8 playlist ...
            var m3uDownloader = new DownloadFileStream(
                restUrl,
                Broadcast.M3uFile,
                Broadcast.GenerateLogFile ? Broadcast.LogFile : null,
                StatusAndProgressHandler);
            // ... then download video from m3u8 file.
            var mp4Downloader = new DownloadVideoStream(
                Broadcast.M3uFile,
                Broadcast.Mp4File,
                Broadcast.GenerateLogFile ? Broadcast.LogFile : null,
                Broadcast.Duration.HasValue ? Broadcast.Duration.Value : TimeSpan.Zero,
                StatusAndProgressHandler);

            // If output file already exists we abord.
            if (File.Exists(Broadcast.Mp4File))
            {
                StatusAndProgressHandler.UpdateStatus($"Filen {mp4Downloader.OutputFile} findes allerede. Slet den hvis du vil downloade igen", EWarningLevel.error);
                return;
            }

            // Begin download of m3u+mp4 files.
            await DownloadAsync(async () =>
            {
                if (!File.Exists(Broadcast.M3uFile))
                {
                    await m3uDownloader.StartAsync();
                }

                if (!File.Exists(Broadcast.M3uFile))
                {
                    await m3uDownloader.StartWithoutHeadAsync();
                }

                if (File.Exists(Broadcast.M3uFile))
                {
                    await mp4Downloader.StartAsync(cts);
                }
            }).ContinueWith(_ =>
            {
                // Cleanup.
                if (Broadcast.DeleteM3uFileAfterDownload)
                {
                    File.Delete(Broadcast.M3uFile);
                }
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

            await StatusAndProgressHandler.UpdateStatusAndWaitAsync("Download start", 1);

            watch.Start();
            await DownloadAsync();
            watch.Stop();

            await StatusAndProgressHandler.UpdateStatusAndWaitAsync("Download slut", 1);
            StatusAndProgressHandler.UpdateStatus("Klar");
            StatusAndProgressHandler.UpdateProgress(0);
        }

    }
}

