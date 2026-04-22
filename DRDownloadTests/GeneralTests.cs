using DRDownload.Common;
using DRDownload.Common.DownloadFile;
using DRDownload.Common.Types.BroadcastHtmlScraper;
using DRDownload.Common.Types.BroadcastTypes;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace DRDownloadTests
{
    [TestClass]
    public sealed class GeneralTests
    {
        [TestMethod]
        public void PointAtCircleArcTest()
        {
            Console.WriteLine(Util.PointAtCircleArc(0f));
            Console.WriteLine(Util.PointAtCircleArc(25f));
            Console.WriteLine(Util.PointAtCircleArc(50f));
            Console.WriteLine(Util.PointAtCircleArc(75f));
            Console.WriteLine(Util.PointAtCircleArc(100f));

            Console.WriteLine(Util.PointAtCircleArc(10f));
            Console.WriteLine(Util.PointAtCircleArc(40f));
            Console.WriteLine(Util.PointAtCircleArc(80f));
        }

        [TestMethod]
        public void DownloadFolderTest()
        {
            Console.WriteLine(Util.WindowsDownloadFolder());
        }

        [TestMethod]
        public void GenerateRadomFlavorIdsTest()
        {
            var randomId = Util.GenerateRadomId("0_", 8, true, true);
            Console.WriteLine(randomId);
        }

        [TestMethod]
        public void GenerateRadomUIConfIdTest()
        {
            var randomId = Util.GenerateRadomId("", 8, true, false);
            Console.WriteLine(randomId);
        }

        [TestMethod]
        public void GenerateSomeBroadcastsFromJSONArrayToDownloadTest()
        {
            var file = @"C:\Users\micha\source\repos\MilkKB\DRDownload\Media\Media.json";

            var result = Util.DeserializeFile<Broadcasts>(file);
        }

        [TestMethod]
        public void CapitalizeTest()
        {
            var text = "raDio";
            Console.WriteLine(text);
            Console.WriteLine(Util.CapitalizedString(text));
        }

        [TestMethod]
        public async Task DownloadBTTest()
        {
            // https://www.kb.dk/find-materiale/dr-arkivet/post/ds.radio:oai:io:b2dd0046-24db-4890-b191-e2cb2e573fb3
            var url = "https://www.kb.dk/find-materiale/dr-arkivet/post/ds.tv:oai:io:90562507-e5e2-4c2e-8a9f-6a09cc9105dd";
            var file = "c:/temp/kb.txt";

            await new DownloadFileStream(url, file).StartAsync();

        }

        // HttpClient lifecycle management best practices:
        // https://learn.microsoft.com/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use
        private static HttpClient sharedClient = new()
        {
            //BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
            BaseAddress = new Uri("https://www.kb.dk"),
        };

        static async Task GetAsync(HttpClient httpClient)
        {
            //using HttpResponseMessage response = await httpClient.GetAsync("todos/3");
            using HttpResponseMessage response = await httpClient.GetAsync("find-materiale/dr-arkivet/post/ds.tv:oai:io:90562507-e5e2-4c2e-8a9f-6a09cc9105dd");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{jsonResponse}\n");

            // Expected output:
            //   GET https://jsonplaceholder.typicode.com/todos/3 HTTP/1.1
            //   {
            //     "userId": 1,
            //     "id": 3,
            //     "title": "fugiat veniam minus",
            //     "completed": false
            //   }
        }

        [TestMethod]
        public async Task DownloadBT2Test()
        {
            await GetAsync(sharedClient);
        }

        [TestMethod]
        public void RegExTest()
        {
            var pattern = @"entry_id/0_[a-z0-9]{8}";

            var text = $"-image: url(&quot;https://api.kltr.nordu.net/p/397/sp/39700/thumbnail/entry_id/0_im2bm9bq/version/100002/ipqwj p   ø 3rcipqwrc pq2iu3rc pq23rucpiq23urc il/entry_id/0_im2sk5bq/versi";

            var regEx = new Regex(pattern);

            var mc = regEx.Matches(text);
            // Answer: entry_id/0_im2bm9bq + ...

        }

        [TestMethod]
        public async Task FindTitleInHtmlTest()
        {
            var reader = new StreamReader("c:/temp/dansk_naturgas_html.txt");

            var htmlDocument = new HtmlDocument();
            htmlDocument.Load(reader);

            var forbiddenValues = new[]
            {
                    "Kontakt os gerne, hvis du har brug for hjælp",
                    "Genveje",
                    "Brug os",
                    "Information"
                }.ToList();

            // Get title.
            var values =
                htmlDocument.
                DocumentNode.
                SelectNodes("//h2").
                Select(node => node.InnerText).
                ToList();

            var value = values.Except(forbiddenValues)?.FirstOrDefault();

            Console.WriteLine(value);


        }

        [TestMethod]
        public void FindBigRecordInHtmlTest()
        {
            var reader = new StreamReader("c:/temp/dansk_naturgas_html.txt");

            var htmlDocument = new HtmlDocument();
            htmlDocument.Load(reader);

            var mainBroadcastNode =
                htmlDocument.
                DocumentNode.
                SelectSingleNode("//div[@class=\"boardcast-record-data\"]");
            var leftSideBroadcastNode =
                mainBroadcastNode?.
                SelectSingleNode("//div[@class=\"main-record-data\"]");
            var rightSideBroadcastNode =
                mainBroadcastNode?.
                SelectSingleNode("//div[@class=\"right-side\"]");

            Console.WriteLine("Title: " + InnerTextOfH2(mainBroadcastNode));
            Console.WriteLine("Description: " + InnerTextOfP(mainBroadcastNode));
            Console.WriteLine("Date: " + InnerTextOfDivHoldingSpanWithClassname(rightSideBroadcastNode, "info", "event"));
            Console.WriteLine("Duration: " + InnerTextOfDivHoldingSpanWithClassname(rightSideBroadcastNode, "info", "schedule"));
            Console.WriteLine("Channel: " + InnerTextOfDivHoldingSpanWithClassname(rightSideBroadcastNode, "info", "tv"));
            Console.WriteLine("Episode: " + InnerTextOfDivHoldingSpanWithClassname(rightSideBroadcastNode, "info", "segment"));
            Console.WriteLine("Genre: " + InnerTextOfLinkWithClassname(rightSideBroadcastNode, "genre-link"));
            Console.WriteLine("EntryId: " + EntryId(StyleOfDivWithClassname(mainBroadcastNode, "playkit-poster")));

        }

        [TestMethod]
        public void FindBigRecordInHtmlFromScraperTest()
        {
            var htmlFile = "c:/temp/dansk_naturgas_html.txt";
            var html = File.ReadAllText(htmlFile);

            var scraper = new BroadcastHtmlScraper("", html);
        }

        [TestMethod]
        public void DRDateToObjectTest()
        {
            var drExpression = "20. maj 2017";

            try
            {
                var date = new ParseDRDate(drExpression).Date;
                Console.WriteLine(date);
            }
            catch (Exception)
            {
            }
        }

        [TestMethod]
        public void DRTimeExpressionToObjectsTest()
        {
            var drExpression = "kl.  14:40 - 15:09 ( 29min 31sek )";

            try
            {
                var p = new ParseDRDuration(drExpression);
                Console.WriteLine(p.From);
                Console.WriteLine(p.To);
                Console.WriteLine(p.Duration);
            }
            catch (Exception)
            {
            }
        }

        private string? EntryId(string? text)
        {
            if (string.IsNullOrEmpty(text))
            { return null; }

            var pattern = @"entry_id/0_[a-z0-9]{8}";
            var regEx = new Regex(pattern);

            var firstMatch = regEx.Match(text);

            if (string.IsNullOrEmpty(firstMatch?.Value))
            { return null; }

            var entryId = firstMatch.Value.Trim().Split('/')?.Last();

            return entryId;
        }

        private string? InnerTextOfH2(HtmlNode? node)
        {
            if (node == null)
            { return null; }

            var value = node.SelectSingleNode("//h2").InnerText;

            return value;
        }

        private string? InnerTextOfP(HtmlNode? node)
        {
            if (node == null)
            { return null; }

            var value = node.SelectSingleNode("//p").InnerText;

            return value;
        }

        private string? InnerTextOfDivHoldingSpanWithClassname(HtmlNode? node, string className, string leadDivInnerTextToRemove)
        {
            if (node == null)
            { return null; }

            var value =
                node.
                SelectNodes($"//div[@class=\"{className}\"]").
                Where(node => node.InnerText.StartsWith(leadDivInnerTextToRemove))?.
                FirstOrDefault()?.
                InnerText.
                TrimStart(leadDivInnerTextToRemove.ToCharArray())?.
                Trim();

            return value;
        }

        private string? StyleOfDivWithClassname(HtmlNode? node, string className)
        {
            if (node == null)
            { return null; }

            var value =
                node.
                SelectSingleNode($"//div[@class=\"{className}\"]")?.
                GetAttributeValue("style", "style not found")?.
                Trim();

            return value;
        }

        private string? InnerTextOfSpanWithClassname(HtmlNode? node, string className)
        {
            if (node == null)
            { return null; }

            var value =
                node.
                SelectSingleNode($"//span[@class=\"{className}\"]")?.
                InnerText?.
                Trim();

            return value;
        }

        private string? InnerTextOfLinkWithClassname(HtmlNode? node, string className)
        {
            if (node == null)
            { return null; }

            var value =
                node.
                SelectSingleNode($"//a[@class=\"{className}\"]")?.
                InnerText?.
                Trim();

            return value;
        }

    }
}


//var title = mrd.SelectSingleNode("//h2").InnerText;
//var description = mrd.SelectSingleNode("//p").InnerText;
//Console.WriteLine(title);
//Console.WriteLine(description);

//Console.WriteLine(SpanInnerText(node, "set-duration"));
//Console.WriteLine(SpanInnerText(node, "episode-text"));
