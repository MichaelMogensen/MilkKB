using DRDownload.Common.DownloadFile;
using DRDownload.Common.DownloadVideo;
using DRDownloadWindow2.Download.KLTRRestAPI;
using DRDownloadWindow2.Types;
using DRDownloadWindow2.Utilities;
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

        public UIDispatchUpdater<UIElementProps<string>> StatusBar { get; set; }
        public UIDispatchUpdater<UIElementProps<int>> ProgressBar { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="broadcast"></param>
        /// <param name="statusBar"></param>
        /// <param name="progressBar"></param>
        public DRMedia(
            Broadcast broadcast, 
            UIDispatchUpdater<UIElementProps<string>> statusBar, 
            UIDispatchUpdater<UIElementProps<int>> progressBar)
        {
            Broadcast = broadcast;
            StatusBar = statusBar;
            ProgressBar = progressBar;
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
                StatusBar.Value = new UIElementProps<string>("Media type skal være enten radio eller tv", EWarningLevel.error);
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
                StatusBar.Value = new UIElementProps<string>("Kan ikke starte radio download med manglende mp3 fil", EWarningLevel.error);
                return;
            }

            var restUrl = new KLTRRestAPIUrlRadio(Broadcast.EntryId).Url;
            var mp3Downloader = new DownloadFileStream(restUrl, Broadcast.Mp3File);

            // If output file already exists we abort.
            if (File.Exists(mp3Downloader.OutputFile))
            {
                StatusBar.Value = new UIElementProps<string>($"Filen {mp3Downloader.OutputFile} findes allerede. Slet den hvis du vil downloade igen", EWarningLevel.warning);
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
                StatusBar.Value = new UIElementProps<string>("Kan ikke starte radio tv med manglende m3u8 fil", EWarningLevel.error);
                return;
            }
            if (string.IsNullOrEmpty(Broadcast.Mp4File))
            {
                StatusBar.Value = new UIElementProps<string>("Kan ikke starte radio tv med manglende mp4 fil", EWarningLevel.error);
                return;
            }
            if (string.IsNullOrEmpty(Broadcast.LogFile))
            {
                StatusBar.Value = new UIElementProps<string>("Kan ikke starte radio tv med manglende log fil", EWarningLevel.error);
                return;
            }
            if (Broadcast.Duration == null || Broadcast.Duration.Value == default)
            {
                StatusBar.Value = new UIElementProps<string>("Kan ikke starte radio tv med manglende total varighed på udsendelse", EWarningLevel.error);
                return;
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
                Broadcast.Duration.Value,
                StatusBar,
                ProgressBar);

            // If output file already exists we abord.
            if (File.Exists(Broadcast.Mp4File))
            {
                StatusBar.Value = new UIElementProps<string>($"Filen {mp4Downloader.OutputFile} findes allerede. Slet den hvis du vil downloade igen", EWarningLevel.warning);
                return;
            }

            // Begin download of m3u+mp4 files.
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

            StatusBar.Value = new UIElementProps<string>("Download start", EWarningLevel.info);
            await Task.Delay(1000);

            watch.Start();
            await DownloadAsync();
            watch.Stop();

            StatusBar.Value = new UIElementProps<string>("Download slut", EWarningLevel.info);
            await Task.Delay(1000);
        }

    }
}

