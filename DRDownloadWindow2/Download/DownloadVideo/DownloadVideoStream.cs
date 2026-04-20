using DRDownload.Common.DownloadVideo.Arguments;
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
        public string? InputFile { get; set; }
        public string? OutputFile { get; set; }
        public string? LogFile { get; set; }

        public TimeSpan? ExpectedTotalMp4Duration { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="inputFile">Expected to be a m3u8 file</param>
        /// <param name="outputFile">Expected to be a mp4 file</param>
        /// <param name="logFile">Log file. No null nothing is logged</param>
        /// <param name="expectedTotalMp4Duration">From metadata the total duration of the video</param>
        public DownloadVideoStream(string? inputFile, string? outputFile, string? logFile, TimeSpan? expectedTotalMp4Duration)
        {
            InputFile = inputFile;
            OutputFile = outputFile;
            LogFile = logFile;
            ExpectedTotalMp4Duration = expectedTotalMp4Duration;
        }

        /// <summary>
        /// Download async.
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cts)
        {
            cts.ThrowIfCancellationRequested();

            try
            {
                if (!string.IsNullOrEmpty(LogFile) && File.Exists(LogFile))
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
                            //progressNotifier.NotifyConsoleBelow100Pct(duration);
                            //logNotifier.LogLine($"DUR: {duration:c}");
                        })
                        .NotifyOnError(msg =>
                        {
                            //logNotifier.LogLine($"MSG: {msg}");
                        })
                        .ProcessAsynchronously(
                            true,
                            new FFOptions { LogLevel = FFMpegLogLevel.Info });

                watch.Stop();

                //progressNotifier.NotifyConsoleAt100Pct();
                //logNotifier.LogLine($"Duration: {watch.Elapsed:c}");
            }
            catch (OperationCanceledException ex)
            {
                //DRMedia.PipeOutput?.PipeMessageTo(ex.Message);
                //logNotifier.LogLine(ex.Message);
            }
            catch (Exception ex)
            {
                //DRMedia.PipeOutput?.PipeMessageTo(ex.Message);
                //logNotifier.LogLine(ex.Message);
            }

        }
    }
}

