using DRDownload.Common.DownloadVideo.Arguments;
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
    /// </summary>
    public class DownloadVideoStream
    {
        public string InputFile { get; private set; }
        public string OutputFile { get; private set; }
        public string LogFile { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="inputFile">Could be m3u8 file or mp4 file etc.</param>
        public DownloadVideoStream(string inputFile)
        {
            InputFile = inputFile;
            OutputFile = Path.ChangeExtension(inputFile, "mp4");
            LogFile = Path.ChangeExtension(inputFile, "log");
        }

        /// <summary>
        /// Download async.
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cts)
        {
            var lockId = Util.GenerateRandomGuid();
            Action<string> LogLine =
                line =>
                {
                    lock (lockId)
                    {
                        File.AppendAllLines(LogFile, new string[] { $"{DateTime.Now:yyyy.MM.dd HH:mm:ss} {line}" }.AsEnumerable());
                    }
                };

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
                        .NotifyOnProgress(timeSpend =>
                        {
                            LogLine($"DUR: {timeSpend:c}");
                        })
                        .NotifyOnError(error =>
                        {
                            LogLine($"MSG: {error}");
                        })
                        .ProcessAsynchronously(
                            true,
                            new FFOptions { LogLevel = FFMpegLogLevel.Info });

                watch.Stop();
                LogLine($"Duration: {watch.Elapsed:c}");
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
                LogLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LogLine(ex.Message);
            }

        }
    }
}

