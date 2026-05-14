using DRDownloadLib.Utilities;

namespace DRDownloadLib.Download.KLTRRestAPI
{
    /// <summary>
    /// API radio url for downloading mp3-file from kaltura server.
    /// </summary>
    public class KLTRRestAPIUrlRadio : KLTRRestAPIUrlBase
    {
        /// <summary>
        /// Ctor. radio.
        /// </summary>
        /// <param name="entryId"></param>
        public KLTRRestAPIUrlRadio(string? entryId)
        {
            EntryId = entryId;
            Format = "url";
            FileOnHost = "a.mp3";
            ClientTag = "html5:v7.194";

            FormatUrl(WithArguments());
        }

        protected override string PageUrl =>
            $"{BaseReferrerUrl}radio:oai:io:{Util.GenerateRandomGuid()}";

        protected override string WithArguments()
        {
            var arguments =
                $"playSessionId={PlaySessionId}" +
                $"&referrer={Referrer}" +
                $"&clientTag={ClientTag}";

            return arguments;
        }
    }
}

