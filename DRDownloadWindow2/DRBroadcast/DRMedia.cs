using DRDownload.Common.DownloadFile;
using DRDownload.Common.DownloadVideo;
using DRDownloadWindow2.Download.KLTRRestAPI;
using DRDownloadWindow2.Types;
using DRDownloadWindow2.Utilities;
using System.Diagnostics;
using File = System.IO.File;

namespace DRDownloadWindow2.DRBroadcast
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
                Broadcast.LogFile,
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
                StatusAndProgressHandler.UpdateStatus("Kan ikke starte tv download med manglende m3u8 fil", EWarningLevel.error);
                return;
            }
            if (string.IsNullOrEmpty(Broadcast.Mp4File))
            {
                StatusAndProgressHandler.UpdateStatus("Kan ikke starte tv download med manglende mp4 fil", EWarningLevel.error);
                return;
            }
            if (Broadcast.Duration == null || Broadcast.Duration.Value == default)
            {
                StatusAndProgressHandler.UpdateStatus("Kan ikke starte tv download med manglende total varighed på udsendelse", EWarningLevel.error);
                return;
            }

            // Prepare m3u8 file download.
            var restUrl = new KLTRRestAPIUrlVideo(Broadcast.EntryId).Url;

            // First download m3u8 playlist ...
            var m3uDownloader = new DownloadFileStream(
                restUrl,
                Broadcast.M3uFile,
                Broadcast.LogFile,
                StatusAndProgressHandler);
            // ... then download video from m3u8 file.
            var mp4Downloader = new DownloadVideoStream(
                Broadcast.M3uFile,
                Broadcast.Mp4File,
                Broadcast.LogFile,
                Broadcast.Duration.Value,
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

            // Cleanup.
            if (!string.IsNullOrEmpty(Broadcast.M3uFile) && Broadcast.M3uFile.StartsWith(Const.DELETE_ME_PREFIX))
            {
                File.Delete(Broadcast.M3uFile);
            }

            await StatusAndProgressHandler.UpdateStatusAndWaitAsync("Download slut", 1);
            StatusAndProgressHandler.UpdateStatus("Klar");
            StatusAndProgressHandler.UpdateProgress(0);
        }

    }
}

