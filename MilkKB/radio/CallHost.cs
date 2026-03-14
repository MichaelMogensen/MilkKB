using MilkKB.radio.types;

namespace MilkKB.radio
{
    public static class CallHost
    {
        public static async Task DownloadStream(ApiParams apiParams)
        {
            try
            {
                if (File.Exists(apiParams.FileOnLocal))
                {
                    File.Delete(apiParams.FileOnLocal);
                }

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.ConnectionClose = true;
                    client.Timeout = TimeSpan.FromMinutes(3);

                    var result = await client.GetAsync(new Uri(apiParams.Url));
                    using (var fs = new FileStream(apiParams.FileOnLocal, FileMode.CreateNew))
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

