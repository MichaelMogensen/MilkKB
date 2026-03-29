using DRDownload.Radio;
using DRDownload.Video;

var cts = new CancellationTokenSource();
await new DRVideo().DownloadVideoBroadcastsAsync(cts.Token);













//await new DRRadio().DownloadRadioBroadcastsAsync();
