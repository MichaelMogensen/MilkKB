namespace DRDownload.Common.Types
{
    /// <summary>
    /// API vidio url for downloading m3u-file. VLC can download mp4-file based on that.
    /// </summary>
    public class DRUrlVideo : DRUrlBase
    {
        /// <summary>
        /// Ctor video.
        /// </summary>
        /// <param name="entryId"></param>
        public DRUrlVideo(string entryId)
        {
            EntryId = entryId;
            Format = "applehttp";
            FileOnHost = "a.m3u8";
            ClientTag = "html5:v3.17.46";

            FormatUrl(WithArguments());
        }

        protected override string PageUrl => 
            $"{BaseReferrerUrl}tv:oai:io:{Util.GenerateRandomGuid()}";

        protected override string WithArguments()
        {
            var arguments =
                $"uiConfId={UIConfId}" + 
                $"&playSessionId={PlaySessionId}" +
                $"&referrer={Referrer}" +
                $"&clientTag={ClientTag}";

            return arguments;
        }
    }
}

