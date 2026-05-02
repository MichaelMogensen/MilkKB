using DRDownloadWindow2.Utilities;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace DRDownload.Common.DownloadFile
{
    /// <summary>
    /// Call REST API and save downloaded file to local system.
    /// 
    /// Sample rest calls to get some TEST json: https://jsonplaceholder.typicode.com
    /// </summary>
    public class DownloadFileStream
    {
        public string Url { get; private set; }
        public string OutputFile { get; private set; }
        public string? LogFile { get; private set; }

        public StatusAndProgressHandler StatusAndProgressHandler { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="outputFile"></param>
        /// <returns></returns>
        public DownloadFileStream(
            string url,
            string outputFile,
            string? logFile,
            StatusAndProgressHandler statusAndProgressHandler)
        {
            Url = url;
            OutputFile = outputFile;
            LogFile = logFile;
            StatusAndProgressHandler = statusAndProgressHandler;
        }

        /// <summary>
        /// Download async.
        /// </summary>
        /// <returns></returns>
        public async Task StartWithoutHeadAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.ConnectionClose = true;
                client.Timeout = TimeSpan.FromMinutes(5);

                var result = await client.GetAsync(new Uri(Url));
                using (var fs = new FileStream(OutputFile, FileMode.CreateNew))
                {
                    await result.Content.CopyToAsync(fs);
                }
            }
        }

        /// <summary>
        /// Download async.
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            var logNotifier = new LogNotifier(LogFile);

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.ConnectionClose = true;
                    client.Timeout = TimeSpan.FromMinutes(5);

                    // Read headers only.
                    using (var response = await client.GetAsync(new Uri(Url), HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();
                        var bytesExpected = response.Content.Headers.ContentLength ?? -1L;
                        if (bytesExpected == -1)
                        {
                            logNotifier.LogLine($"response.Content.Headers.ContentLength = {bytesExpected} when requesting {Url}. Download aborted.");

                            return;
                        }

                        // Read stream.
                        StatusAndProgressHandler.UpdateStatus($"Henter {OutputFile}");

                        using (var inputFileStream = await response.Content.ReadAsStreamAsync())
                        {
                            var buffer = new byte[8192];
                            var bytesRead = 0L;
                            var bytesReadTotal = 0L;

                            // Write stream.
                            using (var outputFileStream = new FileStream(OutputFile, FileMode.Append, FileAccess.Write))
                            {
                                logNotifier.LogLine($"Download start of {OutputFile}. Expected to be {bytesExpected} bytes.");
                                var calcProgress = new CalcProgressBasedOnBytes(bytesExpected);

                                var watch = new Stopwatch();
                                watch.Start();

                                while (true)
                                {
                                    bytesRead = await inputFileStream.ReadAsync(buffer, 0, buffer.Length);
                                    if (bytesRead == 0)
                                    {
                                        // Nothing more to read.
                                        break;
                                    }
                                    else
                                    {
                                        // Continue read/write.
                                        outputFileStream.Write(buffer);
                                        bytesReadTotal += bytesRead;

                                        if (calcProgress.Calc(bytesReadTotal))
                                        {
                                            StatusAndProgressHandler.UpdateProgress(calcProgress.Value);

                                            logNotifier.LogLine($"(bytesRead, bytesReadTotal, %) = ({bytesRead}, {bytesReadTotal}, {calcProgress.Value}%)");
                                        }
                                    }
                                }

                                watch.Stop();

                                // Ensure at 100% after download.
                                logNotifier.LogLine($"Duration: {watch.Elapsed:c}");
                                StatusAndProgressHandler.UpdateProgress(100);
                                await StatusAndProgressHandler.UpdateStatusAndWaitAsync($"Det tog {watch.Elapsed.TotalSeconds:0}s at hente filen", 6);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StatusAndProgressHandler.UpdateStatus("Ukendt problem. Genstart programmet");
                logNotifier.LogLine(ex.Message);
            }
        }

    }
}

