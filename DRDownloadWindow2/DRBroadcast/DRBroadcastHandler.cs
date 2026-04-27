using DRDownloadWindow2.Types;

namespace DRDownloadWindow2.DRBroadcast
{
    public class DRBroadcastHandler
    {
        private ChromeBrowser? Browser { get; set; }

        public Broadcast Broadcast { get; set; } = new Broadcast();

        /// <summary>
        /// Ctor.
        /// </summary>
        public DRBroadcastHandler(ChromeBrowser? browser)
        {
            Browser = browser;
        }

        /// <summary>
        /// If page has broadcast details full broadcast object is constructed here. If not, object has only its Url 
        /// set and rest of properties is null.
        /// </summary>
        /// <returns></returns>
        public void ReadBroadcastDetails()
        {
            if (Browser == null)
            { return; }

            var isPageExpectedToHoldAnyMediaDetails = DRBroadcastHtmlScraper.MediaTypeByUrl(Browser.Url) != EMediaType.nomedia;

            var html = isPageExpectedToHoldAnyMediaDetails ?
                Browser.GetPageHtml(DRBroadcastHtmlScraper.BROADCAST_REC_DATA_PARENT_XPATH) : // Wait this XPATH that holds broadcast details.
                Browser.GetPageHtml(); // Holds crab not relevant for us.

            Broadcast = new DRBroadcastHtmlScraper(Browser.Url, html).Broadcast;
        }
    }
}

