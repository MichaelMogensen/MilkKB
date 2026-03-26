using MilkKB.radio;
using System.Drawing;

await new RadioBroadcasts().DownloadRadioBroadcastsAsync();

//var m3u8Url = @"https://api.kltr.nordu.net/p/397/sp/39700/playManifest/entryId/0_ela5z5u2/protocol/https/format/applehttp/flavorIds/0_eslae4ee/a.m3u8?uiConfId=23454143&playSessionId=f30a8bfb-0795-9b0c-3141-d540cb8e7cf7:d399be37-2665-5d39-0b52-2d35d3b3d559&referrer=aHR0cHM6Ly93d3cua2IuZGsvZmluZC1tYXRlcmlhbGUvZHItYXJraXZldC9wb3N0L2RzLnR2Om9haTppbzpkZGQwNjVmZi1iNDdmLTQ2MTYtYmRhYi01OTg3YzBjZTcyMmU=&clientTag=html5:v3.17.46";
//await MilkTvBroadcasts.Fetch_m3u8_file_Async(m3u8Url);

// TODO: Read mp4url from m3u8Url.

var videoUrl = @"https://vod-cache.kaltura.nordu.net/hls/p/397/sp/39700/serveFlavor/entryId/0_ela5z5u2/v/12/ev/1/flavorId/0_eslae4ee/name/a.mp4/index.m3u8";

//await Task.Run(async () =>
//{
//    using (var broadcasts = new TvBroadcasts(videoUrl))
//    {
//        broadcasts.LogBackends();
//        broadcasts.StartVideoRecording();
//    } // On dispose: StopVideoRecording();

//});

static byte[] ImageToByte(Image img)
{
    ImageConverter converter = new ImageConverter();
    return (byte[])converter.ConvertTo(img, typeof(byte[]));
}

// OK: var capture = new VideoCapture("https://test-videos.co.uk/vids/bigbuckbunny/mp4/h264/1080/Big_Buck_Bunny_1080_10s_1MB.mp4");
//var capture = new VideoCapture(@"C:\Temp\otto.m3u");
//var mat = new Mat();
//capture.Read(mat);
//var bmp = mat.ToBitmap();
//File.WriteAllBytes(@"c:\temp\otto.bmp", ImageToByte(bmp));


async Task DownloadStream()
{
    try
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            client.Timeout = TimeSpan.FromMinutes(3);

            var result = await client.GetAsync(new Uri(@"C:\Temp\otto.m3u"));
            using (var fs = new FileStream(@"c:\temp\otto2.mp4", FileMode.CreateNew))
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

await DownloadStream();

Console.ReadKey();