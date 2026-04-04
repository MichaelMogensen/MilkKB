using DRDownload.Common;
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


    }
}


