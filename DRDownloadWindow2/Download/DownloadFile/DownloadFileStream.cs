
using System.IO;
using System.Net.Http;

namespace DRDownload.Common.DownloadFile
{
    /// <summary>
    /// Call REST API and save downloaded file to local system.
    /// 
    /// Sample rest calls to get some json: https://jsonplaceholder.typicode.com
    /// 
    /// </summary>
    public class DownloadFileStream
    {
        public string Url { get; private set; }
        public string OutputFile { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public DownloadFileStream(string url, string file)
        {
            Url = url;
            OutputFile = file;
        }

        /// <summary>
        /// Download async.
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            try
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
            catch (Exception)
            {
                //DRMedia.PipeOutput?.PipeMessageTo(ex.Message);
            }
        }
    }
}

