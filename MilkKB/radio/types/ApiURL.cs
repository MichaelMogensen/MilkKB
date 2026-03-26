namespace MilkKB.radio.types
{
    /// <summary>
    /// 
    /// Id's look like...
    /// 
    /// PartnerId = 397
    /// UIConfId = 39700
    /// EntryId = 0_ela5z5u2
    /// 
    /// </summary>
    public class ApiURL
    {
        private readonly static int PartnerId = 397;
        private readonly static int UIConfId = 39700;

        private readonly static string BaseUrl = 
            $"https://api.kltr.nordu.net/p/{PartnerId}/sp/{UIConfId}/playManifest/";       

        public string Result { get; set; } = string.Empty;

        public ApiURL(ApiParams apiParams)
        {
            BuildURL(apiParams);
        }

        private void BuildURL(ApiParams apiParams)
        {
            Result =
                BaseUrl +
                $"entryId/{apiParams.EntryId}/" +
                $"protocol/https/" +
                $"format/url/" +
                $"flavorIds/{apiParams.FlavorIds}/" +
                $"{apiParams.FileOnHost}" +
                $"?playSessionId={apiParams.PlaySessionId}" +
                $"&referrer={apiParams.Referrer}" +
                $"&clientTag=html5:v7.194";
        }
    }
}

