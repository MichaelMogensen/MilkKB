using DRDownload.Common.DownloadVideo.Arguments;
using DRDownloadWindow2.Download;
using DRDownloadWindow2.Types;
using FFMpegCore;
using FFMpegCore.Enums;
using System.Diagnostics;
using System.IO;

namespace DRDownload.Common.DownloadVideo
{
    /// <summary>
    /// Based on FFMpeg.
    /// 
    /// Download mp4 file from m3u playlist.
    /// 
    /// ffmpeg flags: https://gist.github.com/tayvano/6e2d456a9897f55025e25035478a3a50
    /// Sample videos for download: https://file-examples.com/storage/fe05737d1f69c8fe396b0e5/2017/04/file_example_MP4_1920_18MG.mp4
    /// 
    /// </summary>
    public class DownloadVideoStream
    {
        private Broadcast Broadcast { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="inputFile">Could be m3u8 file or mp4 file etc.</param>
        /// <param name="durationVideo">From metadata the total duration of the video</param>
        public DownloadVideoStream(Broadcast broadcast)
        {
            Broadcast = broadcast;
        }

        /// <summary>
        /// Download async.
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cts)
        {
            if (Broadcast.M3uFile == null)
            {
                throw new ArgumentNullException($"{nameof(Broadcast.M3uFile)} == null");
            }
            if (Broadcast.Mp4File == null)
            {
                throw new ArgumentNullException($"{nameof(Broadcast.Mp4File)} == null");
            }
            if (Broadcast.LogFile == null)
            {
                throw new ArgumentNullException($"{nameof(Broadcast.LogFile)} == null");
            }
            if (Broadcast.Duration == null)
            {
                throw new ArgumentNullException($"{nameof(Broadcast.Duration)} == null");
            }

            var logNotifier = new OngoingDownloadLogNotifier(Broadcast.LogFile);
            var progressNotifier = new OngoingDownloadProgressNotifier(Broadcast.Duration.Value);

            cts.ThrowIfCancellationRequested();

            try
            {
                if (File.Exists(Broadcast.LogFile))
                {
                    File.Delete(Broadcast.LogFile);
                }

                var watch = new Stopwatch();
                watch.Start();

                var ff = await FFMpegArguments
                    .FromFileInput(Broadcast.M3uFile, true, op => op
                        .WithArgument(new ProtocolWhitelistArgument())
                        .WithArgument(new OverwriteOutputfileArgument()))
                    .OutputToFile(Broadcast.Mp4File, true, op => op
                        .WithFastStart())
                        .CancellableThrough(cts)
                        .NotifyOnProgress(duration =>
                        {
                            progressNotifier.NotifyConsoleBelow100Pct(duration);
                            logNotifier.LogLine($"DUR: {duration:c}");
                        })
                        .NotifyOnError(msg =>
                        {
                            logNotifier.LogLine($"MSG: {msg}");
                        })
                        .ProcessAsynchronously(
                            true,
                            new FFOptions { LogLevel = FFMpegLogLevel.Info });

                watch.Stop();

                progressNotifier.NotifyConsoleAt100Pct();
                logNotifier.LogLine($"Duration: {watch.Elapsed:c}");
            }
            catch (OperationCanceledException ex)
            {
                //DRMedia.PipeOutput?.PipeMessageTo(ex.Message);
                logNotifier.LogLine(ex.Message);
            }
            catch (Exception ex)
            {
                //DRMedia.PipeOutput?.PipeMessageTo(ex.Message);
                logNotifier.LogLine(ex.Message);
            }

        }
    }
}

