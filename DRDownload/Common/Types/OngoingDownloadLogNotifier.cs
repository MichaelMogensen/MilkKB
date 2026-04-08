namespace DRDownload.Common.Types
{
    public class OngoingDownloadLogNotifier
    {
        private static readonly string LockId = Util.GenerateRandomGuid();

        private string LogFile { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="logFile"></param>
        public OngoingDownloadLogNotifier(string logFile)
        {
            LogFile = logFile;
        }

        /// <summary>
        /// Log with timestamp.
        /// </summary>
        /// <param name="line"></param>
        public void LogLine(string line)
        {
            lock (LockId)
            {
                File.AppendAllLines(LogFile, new string[] { $"{DateTime.Now:yyyy.MM.dd HH:mm:ss} {line}" }.AsEnumerable());
            }
        }

    }
}

