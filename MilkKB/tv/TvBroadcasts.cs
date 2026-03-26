using Emgu.CV;
using Emgu.CV.Dnn;
using MilkKB.tv.types;

namespace MilkKB.tv
{
    public class TvBroadcasts : IDisposable
    {
        public VideoParams Params { get; set; }

        public TvBroadcasts(string url)
        {
            Params = new VideoParams(url);
        }

        public static async Task Fetch_m3u8_file_Async(string url)
        {
            using (var client = new HttpClient())
            {
                var videoBytes = await client.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(@"c:\temp\otto.m3u", videoBytes);
            }
        }

        public void LogBackends()
        {
            var lines = CvInvoke.WriterBackends.Select(b => b.Name);
            File.WriteAllLines(@"c:\temp\otto.log", lines);
        }

        public void StartVideoRecording()
        {
            Params.Reader.ImageGrabbed += OnCaptureImage;
            Params.Reader.Start();
        }

        private void OnCaptureImage(object? sender, EventArgs e)
        {
            Params.Reader.Retrieve(Params.Frame);
            CvInvoke.Resize(Params.Frame, Params.ResizedFrame, Params.VideoSize);
            Params.Writer.Write(Params.ResizedFrame);
        }

        private void StopVideoRecording()
        {
            if (Params.Reader != null)
            {
                Params.Reader.Stop();
                Params.Reader.Dispose();
            }
        }

        public void Dispose()
        {
            StopVideoRecording();
        }

    }
}

// Video to explain this: https://www.youtube.com/watch?v=VyqVJLecXyk

