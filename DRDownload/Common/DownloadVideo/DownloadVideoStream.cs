using DRDownload.Common.DownloadVideo.Arguments;
using DRDownload.Common.Types;
using DRDownload.Media;
using FFMpegCore;
using FFMpegCore.Enums;
using System.Diagnostics;

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
        public string InputFile { get; private set; }
        public string OutputFile { get; private set; }
        public string LogFile { get; private set; }

        public TimeSpan DurationVideo { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="inputFile">Could be m3u8 file or mp4 file etc.</param>
        /// <param name="outputFile"></param>
        /// <param name="logFile"></param>
        /// <param name="durationVideo">From metadata the total duration of the video</param>
        public DownloadVideoStream(string inputFile, string outputFile, string logFile, TimeSpan durationVideo)
        {
            InputFile = inputFile;
            OutputFile = Path.ChangeExtension(inputFile, "mp4");
            LogFile = Path.ChangeExtension(inputFile, "log");

            DurationVideo = durationVideo;
        }

        /// <summary>
        /// Download async.
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cts)
        {
            var logNotifier = new OngoingDownloadLogNotifier(LogFile);
            var progressNotifier = new OngoingDownloadProgressNotifier(DurationVideo);

            cts.ThrowIfCancellationRequested();

            try
            {
                if (File.Exists(OutputFile))
                {
                    File.Delete(OutputFile);
                }
                if (File.Exists(LogFile))
                {
                    File.Delete(LogFile);
                }

                var watch = new Stopwatch();
                watch.Start();

                var ff = await FFMpegArguments
                    .FromFileInput(InputFile, true, op => op
                        .WithArgument(new ProtocolWhitelistArgument())
                        .WithArgument(new OverwriteOutputfileArgument()))
                    .OutputToFile(OutputFile, true, op => op
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
                DRMedia.PipeOutput?.PipeMessageTo(ex.Message);
                logNotifier.LogLine(ex.Message);
            }
            catch (Exception ex)
            {
                DRMedia.PipeOutput?.PipeMessageTo(ex.Message);
                logNotifier.LogLine(ex.Message);
            }

        }
    }
}

