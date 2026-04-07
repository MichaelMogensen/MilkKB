using DRDownload.Common;
using DRDownload.Common.DownloadFile;
using DRDownload.Common.DownloadVideo;
using DRDownload.Common.Types;
using DRDownload.Common.Types.BroadcastFiles;
using DRDownload.Common.Types.BroadcastTypes;
using DRDownload.Common.Types.RestAPI;
using System.Diagnostics;
using File = System.IO.File;

namespace DRDownload.Media
{
    /// <summary>
    /// Top class to download broadcasts from https://www.kb.dk/find-materiale/dr-arkivet/ by EntryId
    /// found on search side using inspect element.
    /// 
    /// 
    /// 
    /// 
    /// </summary>
    public class DRMedia
    {
        private string DownloadFolder = Util.DownloadFolder();
        public Broadcasts? Broadcasts { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="downloadFolder"></param>
        /// <param name="broadcastsAsJson"></param>
        public DRMedia(string broadcastsAsJson)
        {
            Broadcasts = Util.DeserializeFile<Broadcasts>(broadcastsAsJson);

            Console.WriteLine(
                $"Download radio/tv from www.kb.dk to {DownloadFolder} based on {Broadcasts?.All?.Count() ?? 0} broadcasts as defined in {broadcastsAsJson}");
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Download media files.
        /// </summary>
        /// <returns></returns>
        public async Task StartMediaDownloadsAsync()
        {
            var cts = new CancellationTokenSource();

            var broadcasts = Broadcasts?.All?.Where(broadcast => broadcast != null)?.ToList();
            if (broadcasts == null || broadcasts.Count == 0)
            {
                Console.WriteLine("No broadcasts found.");
            }
            else
            {
                foreach (var broadcast in broadcasts)
                {
                    if (broadcast.MediaType == EMediaType.tv)
                        await StartTvDownloadAsync(cts.Token, broadcast);
                    else
                        if (broadcast.MediaType == EMediaType.radio)
                            await StartRadioDownloadAsync(cts.Token, broadcast);
                }
            }

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
            var prompt = new BroadcastPrompt(broadcast.MediaType);

            var mp3Downloader = new DownloadFileStream(
                new RestAPIUrlRadio(broadcast.EntityId).Url,
                new MP3BroadcastFile(DownloadFolder, broadcast).File);

            if (File.Exists(mp3Downloader.File))
            {
                Console.WriteLine(prompt.AlreadyDownloadedMessage(mp3Downloader.File, DownloadFolder));
                return;
            }

            Console.WriteLine(prompt.BeginDownloadMessage(mp3Downloader.File, DownloadFolder));

            var watch = new Stopwatch();
            watch.Start();

            await mp3Downloader.StartAsync();

            watch.Stop();

            Console.WriteLine(prompt.EndDownloadMessage(mp3Downloader.File, DownloadFolder, watch.Elapsed));
            Console.WriteLine();
        }

        /// <summary>
        /// Start tv download.
        /// </summary>
        /// <param name="cts"></param>
        /// <param name="broadcast"></param>
        /// <returns></returns>
        public async Task StartTvDownloadAsync(CancellationToken cts, Broadcast broadcast)
        {
            // Talk to user.
            var prompt = new BroadcastPrompt(broadcast.MediaType);

            // Prepare m3u8 file download.
            var url = new RestAPIUrlVideo(broadcast.EntityId).Url;
            var m3u8File = new M3U8BroadcastFile(DownloadFolder, broadcast).File;

            // Download m3u8 playlist and video.
            var m3uDownloader = new DownloadFileStream(url, m3u8File);
            var mp4Downloader = new DownloadVideoStream(m3u8File);

            if (File.Exists(mp4Downloader.OutputFile))
            {
                // XXX
                Console.WriteLine();
                return;
            }

            // XXX
            Console.WriteLine();

            var watch = new Stopwatch();
            watch.Start();

            if (!File.Exists(m3u8File))
            {
                await m3uDownloader.StartAsync();
            }
            await mp4Downloader.StartAsync(cts);

            watch.Stop();

            // XXX
            Console.WriteLine();
        }

    }
}

