namespace DRDownload.Common
{
    public abstract class DRRadioAndVideoBase
    {
        /// <summary>
        /// Call API and save to file on local system.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileOnLocal"></param>
        /// <returns></returns>
        public async Task DownloadStreamAsync(string url, string fileOnLocal)
        {
            try
            {
                if (File.Exists(fileOnLocal))
                {
                    File.Delete(fileOnLocal);
                }

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.ConnectionClose = true;
                    client.Timeout = TimeSpan.FromMinutes(5);

                    var result = await client.GetAsync(new Uri(url));
                    using (var fs = new FileStream(fileOnLocal, FileMode.CreateNew))
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

