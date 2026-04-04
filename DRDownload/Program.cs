using DRDownload.Media;

if (args.Length != 1)
{
    Console.WriteLine("Usage: arg. 1 = <json-file holding defined broadcasts>");
    return;
}

await new DRMedia(broadcastsAsJson: args[0]).StartMediaDownloadsAsync();

