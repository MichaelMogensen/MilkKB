using DRDownload.Radio;
using DRDownload.Video;

Console.WriteLine("Downloading radio/video broadcasts...");

//await new DRRadio().DownloadRadioBroadcastsAsync();
await new DRVideo().DownloadVideoBroadcastsAsync();

Console.WriteLine("Done...");

