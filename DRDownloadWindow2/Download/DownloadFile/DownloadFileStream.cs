using DRDownloadWindow2.Utilities;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Net.Http;
using System.Reflection.Emit;
using System.Security.Policy;

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
        public string LogFile { get; private set; }

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
            string logFile,
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
        public async Task StartAsync()
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
        public async Task StartAndFollowProgressAsync()
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
                            // TODO: Status = warning.
                            logNotifier.LogLine($"No bytes to download when requesting {Url}. Download aborted.");
                            return;
                        }

                        // Read stream.
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
                                            StatusAndProgressHandler.UpdateStatus($"{calcProgress.Value}% downloaded");
                                            StatusAndProgressHandler.UpdateProgress(calcProgress.Value);

                                            logNotifier.LogLine($"(bytesRead, bytesReadTotal, %) = ({bytesRead}, {bytesReadTotal}, {calcProgress.Value}%)");
                                        }
                                    }
                                }

                                watch.Stop();

                                // Ensure at 100% after download.
                                await StatusAndProgressHandler.UpdateStatusAndWaitAsync($"Varighed {watch.Elapsed:c}", 2);
                                StatusAndProgressHandler.UpdateProgress(calcProgress.Value);
                                logNotifier.LogLine($"Duration: {watch.Elapsed:c}");
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

        /// <summary>
        /// Something like this.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task DownloadWithProgressAsync(string url, IProgress<double> progress)
        {
            using (var client = new HttpClient())
            {
                // 1. Read headers only.
                using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    var totalBytes = response.Content.Headers.ContentLength ?? -1L;

                    // 2. Read stream.
                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        var buffer = new byte[8192];
                        var totalRead = 0L;
                        var isMoreToRead = true;

                        while (isMoreToRead)
                        {
                            var read = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                            if (read == 0)
                            {
                                isMoreToRead = false;
                            }
                            else
                            {
                                totalRead += read;
                                // 3. Report Progress
                                if (totalBytes != -1)
                                {
                                    progress.Report((double)totalRead / totalBytes * 100);
                                }
                            }

                            // TODO: Save buffer...
                        }
                    }
                }
            }
        }


    }
}

