using DRDownload.Common;
using DRDownload.Common.Types;

namespace DRDownloadTests
{
    [TestClass]
    public sealed class GeneralTests
    {
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
        public void GenerateAPIUrlForRadioTest()
        {
            var entryId = Util.GenerateRadomId("0_", 8, true, true);

            var url = new RestAPIUrlRadio(entryId).Url;
            Console.WriteLine(url);
        }

        [TestMethod]
        public void GenerateAPIUrlForVidioTest()
        {
            var entryId = Util.GenerateRadomId("0_", 8, true, true);

            var url = new RestAPIUrlVideo(entryId).Url;
            Console.WriteLine(url);
        }

    }
}


