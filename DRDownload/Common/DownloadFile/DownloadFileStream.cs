namespace DRDownload.Common.DownloadFile
{
    /// <summary>
    /// Call REST API and save downloaded file to local system.
    /// </summary>
    public class DownloadFileStream
    {
        public string Url { get; set; }
        public string File { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public DownloadFileStream(string url, string file)
        {
            Url = url;
            File = file;
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
                    using (var fs = new FileStream(File, FileMode.CreateNew))
                    {
                        await result.Content.CopyToAsync(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

