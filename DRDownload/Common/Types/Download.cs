namespace DRDownload.Common.Types
{
    public static class Download
    {
        /// <summary>
        /// Call REST API and save to file on local system.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task DownloadStreamAsync(string url, string file)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.ConnectionClose = true;
                    client.Timeout = TimeSpan.FromMinutes(5);

                    var result = await client.GetAsync(new Uri(url));
                    using (var fs = new FileStream(file, FileMode.CreateNew))
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

