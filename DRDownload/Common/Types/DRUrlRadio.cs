namespace DRDownload.Common.Types
{
    /// <summary>
    /// API radio url for downloading mp3-file.
    /// </summary>
    public class DRUrlRadio : DRUrlBase
    {
        /// <summary>
        /// Ctor. radio.
        /// </summary>
        /// <param name="entryId"></param>
        public DRUrlRadio(string entryId)
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

