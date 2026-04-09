using DRDownload.Common;
using DRDownload.Common.DownloadFile;
using DRDownload.Common.Types.BroadcastTypes;

namespace DRDownloadTests
{
    [TestClass]
    public sealed class GeneralTests
    {
        [TestMethod]
        public void DownloadFolderTest()
        {
            Console.WriteLine(Util.DownloadFolder());
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
            Console.WriteLine(Util.Capitalized(text));
        }

        [TestMethod]
        public async Task DownloadBTTest()
        {
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


    }
}

