using MilkKB.types;
using System.Globalization;

namespace MilkKBTests
{
    [TestClass]
    public sealed class GeneralTests
    {
        [TestMethod]
        public void DateTimeFormatsTest()
        {
            var daDK = new CultureInfo("da-DK");

            var d = new DateTime(1989, 3, 15);
            var date = d.ToString("d. MMMM yyyy", daDK);

            Console.WriteLine(date);
        }

        [TestMethod]
        public void LocalFilenameTest()
        {
            var localFilename = new LocalFilename(@"c:\temp", "mp3", new BroadcastMetadata("P4 part one", new DateTime(1989, 1, 1, 19, 0, 0), TimeSpan.FromMinutes(45), "P4"));
        
            Console.WriteLine(localFilename.File);
        }
    }
}

