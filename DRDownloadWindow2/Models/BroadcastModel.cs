using DRDownloadWindow.Types;
using DRDownloadWindow.Utilities;

namespace DRDownloadWindow.Models
{
    public class BroadcastModel
    {
        public static readonly string BaseUrl = "https://www.kb.dk/find-materiale/dr-arkivet/";

        public Broadcast Broadcast { get; set; } = new Broadcast { Url = BaseUrl };

        public ChromeBrowser? Browser { get; set; } =
#if WITH_BROWSER
            new ChromeBrowser();
#else
            null;
#endif

        public BroadcastModel()
        {
            // First time we goto base url.
            Browser?.Url = Broadcast.Url;
        }
    }
}

