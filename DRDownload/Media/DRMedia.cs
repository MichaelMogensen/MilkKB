using DRDownload.Common;
using DRDownload.Common.DownloadFile;
using DRDownload.Common.DownloadVideo;
using DRDownload.Common.Types;
using DRDownload.Common.Types.BroadcastFiles;
using DRDownload.Common.Types.BroadcastTypes;
using DRDownload.Common.Types.RestAPI;
using DRDownload.Pipe;
using System.Diagnostics;
using File = System.IO.File;

namespace DRDownload.Media
{
    /// <summary>
    /// Top class to download broadcasts from https://www.kb.dk/find-materiale/dr-arkivet/ by entry_id
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

        public static PipeOutputBase? PipeOutput { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="downloadFolder"></param>
        /// <param name="broadcastsAsJson"></param>
        /// <param name="PipeOutputBase">Tell where output should go depending on how is using this class</param>
        public DRMedia(string broadcastsAsJson, PipeOutputBase pipeOutput)
        {
            Broadcasts = Util.DeserializeFile<Broadcasts>(broadcastsAsJson);
            PipeOutput = pipeOutput;

            PipeOutput?.PipeMessageTo(
                $"Download radio/tv from www.kb.dk to {DownloadFolder} based on {Broadcasts?.All?.Count() ?? 0} broadcasts as defined in {broadcastsAsJson}");
            PipeOutput?.PipeMessageTo();
            PipeOutput?.PipeMessageTo();
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
                PipeOutput?.PipeMessageTo("No broadcasts found.");
                return;
            }

            // Iterate broadcasts in json file.
            foreach (var broadcast in broadcasts)
            {
                // Ensure no disturbing characters anywhere.
                var broadcastClean = TrimBroadcast(broadcast);

                // Split tv/radio download.
                if (broadcast.MediaType == EMediaType.tv)
                    await StartTvDownloadAsync(cts.Token, broadcastClean);
                else
                    if (broadcast.MediaType == EMediaType.radio)
                        await StartRadioDownloadAsync(cts.Token, broadcastClean);
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
            var messageNotifier = new OngoingDownloadMessageNotifier(broadcast.MediaType);

            var mp3Downloader = new DownloadFileStream(
                new RestAPIUrlRadio(broadcast.EntityId).Url,
                new MP3BroadcastFile(DownloadFolder, broadcast).OutputFile);

            // If output file already exists we abord.
            if (File.Exists(mp3Downloader.OutputFile))
            {
                PipeOutput?.PipeMessageTo(messageNotifier.AlreadyDownloadedMessage(mp3Downloader.OutputFile, DownloadFolder));
                return;
            }

            await DownloadAsync(messageNotifier, mp3Downloader.OutputFile, mp3Downloader.StartAsync);
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
            var messageNotifier = new OngoingDownloadMessageNotifier(broadcast.MediaType);

            // Prepare m3u8 file download.
            var url = new RestAPIUrlVideo(broadcast.EntityId).Url;
            var m3u8File = new M3U8BroadcastFile(DownloadFolder, broadcast).OutputFile;

            // Download m3u8 playlist and video.
            var m3uDownloader = new DownloadFileStream(url, m3u8File);
            var mp4Downloader = new DownloadVideoStream(m3u8File, broadcast.Duration);

            // If output file already exists we abord.
            if (File.Exists(mp4Downloader.OutputFile))
            {
                PipeOutput?.PipeMessageTo(messageNotifier.AlreadyDownloadedMessage(mp4Downloader.OutputFile, DownloadFolder));
                return;
            }

            await DownloadAsync(messageNotifier, mp4Downloader.OutputFile, async () =>
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
            PipeOutput?.PipeMessageTo(messageNotifier.BeginDownloadMessage(file, DownloadFolder));

            var watch = new Stopwatch();
            watch.Start();

            await DownloadAsync();

            watch.Stop();

            PipeOutput?.PipeMessageTo(messageNotifier.EndDownloadMessage(file, DownloadFolder, watch.Elapsed));
        }

        /// <summary>
        /// Avoid special characters.
        /// </summary>
        /// <param name="broadcast"></param>
        /// <returns></returns>
        private static Broadcast TrimBroadcast(Broadcast broadcast)
        {
            var broadcastClean = new Broadcast
            {
                MediaType = broadcast.MediaType,
                EntityId = broadcast.EntityId,
                Title = broadcast.Title?.Trim(":\\".ToCharArray()), // : and \ is not allowed in filenames.
                SendDate = broadcast.SendDate,
                DurationMin = broadcast.DurationMin,
                Channel = broadcast.Channel,
                Description = broadcast.Description?.TrimEnd(".".ToCharArray()) // . looks silly just before extension.
            };

            // Replace things like "2/6" with "2 af 6".
            broadcastClean.Title = broadcastClean.Title?.Replace("/", " af "); // / is not allowed in filenames.

            return broadcastClean;
        }
    }
}

