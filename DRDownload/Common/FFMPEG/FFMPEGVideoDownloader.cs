using DRDownload.Common.FFMPEG.Arguments;
using FFMpegCore;
using FFMpegCore.Enums;

namespace DRDownload.Common.FFMPEG
{
    /// <summary>
    /// ffmpeg flags: https://gist.github.com/tayvano/6e2d456a9897f55025e25035478a3a50
    /// Sample videos for download: https://file-examples.com/storage/fe05737d1f69c8fe396b0e5/2017/04/file_example_MP4_1920_18MG.mp4
    /// </summary>
    public class FFMPEGVideoDownloader
    {
        public string InputFile { get; private set; }
        public string OutputFile { get; private set; }
        public string LogFile { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="m3u8File"></param>
        public FFMPEGVideoDownloader(string m3u8File)
        {
            InputFile = m3u8File;
            OutputFile = Path.ChangeExtension(m3u8File, "mp4");
            LogFile = Path.ChangeExtension(m3u8File, "log");
        }

        public async Task DownloadVideoAsync(CancellationToken cts)
        {
            if (File.Exists(OutputFile))
            {
                File.Delete(OutputFile);
            }
            if (File.Exists(LogFile))
            {
                File.Delete(LogFile);
            }

            await DownloadVideoAsyncInner(cts);
        }

        /// <summary>
        /// For awiting download.
        /// </summary>
        /// <returns></returns>
        public async Task DownloadVideoAsyncInner(CancellationToken cts)
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
                var ts = new TimeSpan(); // TODO: How?
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
                        .NotifyOnProgress(percentageCreated =>
                        {
                            LogLine($"PRO: {percentageCreated:F}%, {ts.TotalSeconds}");
                        }, ts)
                        .NotifyOnOutput(output =>
                        {
                            LogLine($"MSG: {output}");
                        })
                        .NotifyOnError(error =>
                        {
                            LogLine($"ERR: {error}");
                        })
                        .ProcessAsynchronously(
                            true,
                            new FFOptions { LogLevel = FFMpegLogLevel.Info });
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

