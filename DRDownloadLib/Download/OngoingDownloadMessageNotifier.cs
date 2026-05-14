using DRDownloadLib.Types;
using DRDownloadLib.Utilities;

namespace DRDownloadLib.Download
{
    /// <summary>
    /// Notify console with simple strings.
    /// </summary>
    public class OngoingDownloadMessageNotifier
    {
        public string MediaType { get; private set; }

        public OngoingDownloadMessageNotifier(EMediaType mediaType)
        {
            MediaType = Util.CapitalizedString(mediaType.ToString());
        }

        public string AlreadyDownloadedMessage(string file, string downloadFolder) =>
            $"{MediaType} broadcast {Path.GetFileName(file)} already exists in {downloadFolder} folder, hence bypassed.";

        public string BeginDownloadMessage(string file, string downloadFolder) =>
            $"{MediaType} broadcast {Path.GetFileName(file)} saved in {downloadFolder} folder. Download begins. Please wait...";

        public string EndDownloadMessage(string file, string downloadFolder, TimeSpan time) =>
            $"{MediaType} broadcast {Path.GetFileName(file)} saved in {downloadFolder} folder. Download ends after {time:c}";

    }
}

