using Emgu.CV;
using System.Drawing;

namespace MilkKB.tv.types
{
    public class VideoParams
    {
        public string VideoFile { get; set; }
        public int Codec { get; set; }
        public Size VideoSize { get; set; }
        public int FramesPerSecond { get; set; }
        public bool ColorImage { get; set; }
        public Mat Frame { get; set; }
        public Mat ResizedFrame { get; set; }

        public VideoCapture Reader { get; set; }
        public VideoWriter Writer { get; set; }

        public VideoParams(string url)
        {
            VideoFile = @"c:\temp\otto.mp4";
            Codec = VideoWriter.Fourcc('m', 'p', '4', 'v');
            VideoSize = new Size(1920, 1080);
            FramesPerSecond = 30;
            ColorImage = true;
            Frame = new Mat();
            ResizedFrame = new Mat();

            Writer = new VideoWriter(VideoFile, Codec, FramesPerSecond, VideoSize, ColorImage);
            Reader = new VideoCapture(url);
        }
    }
}

