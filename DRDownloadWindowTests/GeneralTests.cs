using DRDownloadLib.Utilities;

namespace DRDownloadWindowTests
{
    [TestClass]
    public class GeneralTests
    {
        [TestMethod]
        public void PrintWindowsDownloadFolderTest()
        {
            Console.WriteLine(Util.WindowsDownloadFolder());
        }

        [TestMethod]
        public void PrintWindowsTempFolderTest()
        {
            Console.WriteLine(Util.WindowsTempFolder());
        }
    }
}

