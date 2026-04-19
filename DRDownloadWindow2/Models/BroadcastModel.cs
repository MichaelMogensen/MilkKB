using DRDownloadWindow2.Types;

namespace DRDownloadWindow2.Models
{
    public class BroadcastModel
    {
        public static readonly string MainSearchPage = "https://www.kb.dk/find-materiale/dr-arkivet/";

        public Broadcast Broadcast { get; set; } = new Broadcast { Url = MainSearchPage };

        public ChromeBrowser Browser { get; set; } = new ChromeBrowser();
    }
}

