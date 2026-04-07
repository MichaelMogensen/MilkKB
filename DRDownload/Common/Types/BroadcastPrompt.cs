using DRDownload.Common.Types.BroadcastTypes;

namespace DRDownload.Common.Types
{
    public class BroadcastPrompt
    {
        public string MediaType { get; private set; }

        public BroadcastPrompt(EMediaType mediaType)
        {
            MediaType = Util.Capitalized(mediaType.ToString());
        }

        public string AlreadyDownloadedMessage(string file, string downloadFolder) =>
            $"{MediaType} broadcast {Path.GetFileName(file)} already exists in {downloadFolder} folder, hence bypassed.";

        public string BeginDownloadMessage(string file, string downloadFolder) =>
            $"{MediaType} broadcast {Path.GetFileName(file)} saved in {downloadFolder} folder. Download begins.";

        public string EndDownloadMessage(string file, string downloadFolder, TimeSpan time) =>
            $"{MediaType} broadcast {Path.GetFileName(file)} saved in {downloadFolder} folder. Download ends after {time:c}";

    }
}

