using DRDownloadLib.DRBroadcast;
using DRDownloadLib.Utilities;

namespace DRDownloadLib.Download.KLTRRestAPI
{
    /// <summary>
    /// API radio/tv base url for downloading mp3/mp4-file from kaltura server.
    /// </summary>
    public abstract class KLTRRestAPIUrlBase
    {
        // Base info. parts. set by base class as fixed.
        private static readonly DRIds DR = new DRIds();

        private static readonly string Protocol = "https";
        private static readonly string Host = $"{Protocol}://api.kltr.nordu.net";
        protected static readonly string BaseReferrerUrl = "https://www.kb.dk/find-materiale/dr-arkivet/post/ds.";

        // Argument parts. set by base class as random.
        protected string FlavorIds => Util.GenerateRadomId("0_", 8, true, true);
        protected string UIConfId => Util.GenerateRadomId("", 8, true, false);
        protected string PlaySessionId => $"{Util.GenerateRandomGuid()}:{Util.GenerateRandomGuid()}";
        protected string Referrer => Util.Base64Encode(PageUrl);

        // Argument parts. set by derived class.
        public string? FileOnHost { get; protected set; }
        public string? EntryId { get; protected set; }
        public string? Format { get; protected set; }
        public string? ClientTag { get; protected set; }

        // Result. Calling API url for fetching m3u-file.
        public string Url { get; protected set; } = string.Empty;

        /// <summary>
        /// Ctor.
        /// </summary>
        protected KLTRRestAPIUrlBase()
        {
        }

        /// <summary>
        /// Format url for API call.
        /// </summary>
        /// <param name="arguments"></param>
        protected void FormatUrl(string arguments)
        {
            Url =
                $"{Host}/" +
                $"p/{DR.PartnerId}/" +
                $"sp/{DR.SPId}/" +
                $"playManifest/" +
                $"entryId/{EntryId}/" +
                $"protocol/{Protocol}/" +
                $"format/{Format}/" +
                $"flavorIds/{FlavorIds}/" +
                $"{FileOnHost}?" +
                $"{arguments}";
        }

        protected abstract string WithArguments();

        protected abstract string PageUrl { get; }
    }
}

