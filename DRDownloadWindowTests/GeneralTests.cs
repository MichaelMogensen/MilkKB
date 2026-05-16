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

        [TestMethod]
        public void SortableTimestampTest()
        {
            var timestamp1 = DateTime.Now.ToString("yyyy.MM.dd.hh.mm");
            Console.WriteLine(timestamp1);
            var timestamp2 = DateTime.Now.ToString("yyyy.MM.dd.HH.mm");
            Console.WriteLine(timestamp2);
        }

        [TestMethod]
        public void FilesystemSafeTest()
        {
            var file = "m:y_f%il\"\"e.txt";
            file = new FilesystemSafe(file).Result;

            Console.WriteLine(file);
        }


    }
}

