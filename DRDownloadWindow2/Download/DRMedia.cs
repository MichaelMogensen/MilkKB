using DRDownload.Common.DownloadFile;
using DRDownload.Common.DownloadVideo;
using DRDownloadWindow2.Download.KLTRRestAPI;
using DRDownloadWindow2.DRBroadcast.DRBroadcastFile;
using DRDownloadWindow2.Types;
using DRDownloadWindow2.Utilities;
using System.Diagnostics;
using File = System.IO.File;

namespace DRDownloadWindow2.Download
{
    /// <summary>
    /// Top class to download broadcasts from https://www.kb.dk/find-materiale/dr-arkivet/ by entry_id
    /// found on search side using inspect element.
    /// 
    /// </summary>
    public class DRMedia
    {
        private string DownloadFolder = Util.WindowsDownloadFolder();

        /// <summary>
        /// Ctor.
        /// </summary>
        public DRMedia()
        {
        }

        /// <summary>
        /// Start radio download.
        /// </summary>
        /// <param name="cts">Not used for radio yet</param>
        /// <param name="broadcast"></param>
        /// <returns></returns>
        public async Task StartRadioDownloadAsync(CancellationToken cts, Broadcast broadcast)
        {
            // Talk to user.
            //var messageNotifier = new OngoingDownloadMessageNotifier(broadcast.MediaType);

            var mp3Downloader = new DownloadFileStream(
                new KLTRRestAPIUrlRadio(broadcast.EntryId).Url,
                new DRMP3BroadcastFile(broadcast).OutputFile);

            // If output file already exists we abord.
            if (File.Exists(mp3Downloader.OutputFile))
            {
                //PipeOutput?.PipeMessageTo(messageNotifier.AlreadyDownloadedMessage(mp3Downloader.OutputFile, DownloadFolder));
                return;
            }

            await DownloadAsync(null/*messageNotifier*/, mp3Downloader.OutputFile, mp3Downloader.StartAsync);
        }

        /// <summary>
        /// Start tv download.
        /// </summary>
        /// <param name="cts"></param>
        /// <param name="broadcast"></param>
        /// <returns></returns>
        public async Task StartTvDownloadAsync(CancellationToken cts, Broadcast broadcast)
        {
            if (broadcast.MediaType == null)
            {
                throw new ArgumentNullException($"{nameof(broadcast.MediaType)} == null");
            }

            // If output file already exists we abord.
            if (File.Exists(broadcast.Mp4File))
            {
                //PipeOutput?.PipeMessageTo(messageNotifier.AlreadyDownloadedMessage(broadcast.Mp4File));
                return;
            }

            // Talk to user.
            var messageNotifier = new OngoingDownloadMessageNotifier(broadcast.MediaType.Value);

            // Prepare m3u8 file download.
            var url = new KLTRRestAPIUrlVideo(broadcast.EntryId).Url;
            var m3u8File = new DRM3U8BroadcastFile(broadcast).OutputFile;

            // Download m3u8 playlist and video.
            var m3uDownloader = new DownloadFileStream(url, m3u8File);
            var mp4Downloader = new DownloadVideoStream(broadcast);

            await DownloadAsync(messageNotifier, "FIX just message: mp4Downloader.OutputFile", async () =>
            {
                if (!File.Exists(m3u8File))
                {
                    await m3uDownloader.StartAsync();
                }
                await mp4Downloader.StartAsync(cts);
            });
        }

        /// <summary>
        /// General timed download for both radio and tv.
        /// </summary>
        /// <param name="messageNotifier"></param>
        /// <param name="file"></param>
        /// <param name="DownloadAsync"></param>
        /// <returns></returns>
        private async Task DownloadAsync(OngoingDownloadMessageNotifier messageNotifier, string file, Func<Task> DownloadAsync)
        {
            //PipeOutput?.PipeMessageTo(messageNotifier.BeginDownloadMessage(file, DownloadFolder));

            var watch = new Stopwatch();
            watch.Start();

            await DownloadAsync();

            watch.Stop();

            //PipeOutput?.PipeMessageTo(messageNotifier.EndDownloadMessage(file, DownloadFolder, watch.Elapsed));
        }

    }
}

