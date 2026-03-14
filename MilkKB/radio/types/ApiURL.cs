namespace MilkKB.radio.types
{
    public class ApiURL
    {
        private readonly static string BaseUrl = "https://api.kltr.nordu.net/p/397/sp/39700/playManifest/";       

        public string Result { get; set; } = string.Empty;

        public ApiURL(ApiParams apiParams)
        {
            BuildURL(apiParams);
        }

        private void BuildURL(ApiParams apiParams)
        {
            Result =
                BaseUrl +
                $"entryId/{apiParams.EntryId.ToString()}/" +
                $"protocol/https/" +
                $"format/url/" +
                $"flavorIds/{apiParams.FlavorIds}/" +
                $"{apiParams.FileOnHost}" +
                $"?playSessionId={apiParams.PlaySessionId.Id1.ToString()}:{apiParams.PlaySessionId.Id2.ToString()}" +
                $"&referrer={apiParams.Referrer}" +
                $"&clientTag=html5:v7.194";
        }
    }
}

