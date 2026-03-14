using MilkKB.types;
using MilkKB.util;

namespace MilkKB.radio.types
{
    public struct ApiParams
    {
        private readonly static string BaseReferrerUrl = "https://www.kb.dk/find-materiale/dr-arkivet/post/ds.radio:oai:io:";

        public string FileOnLocal { get; private set; }
        public string FileOnHost { get; private set; }
        public string EntryId { get; private set; } // 10 chars from <video>...</video> on website.
        public string FlavorIds { get; private set; } = "0_banana_0"; // 10 chars. General one seems to be ok.

        public string PlaySessionId => $"{Guid.NewGuid().ToString()}:{Guid.NewGuid().ToString()}";

        public string Referrer { get; private set; } // Page url to base64.

        public string Url => new ApiURL(this).Result;

        public ApiParams(LocalFilename fileOnLocal, string fileOnHost, string entryId)
        {
            FileOnLocal = fileOnLocal.Result;
            FileOnHost = fileOnHost;
            EntryId = entryId;

            var pageUrl = $"{BaseReferrerUrl}{Guid.NewGuid().ToString()}";
            Referrer = Util.Base64Encode(pageUrl);
        }
    }
}

