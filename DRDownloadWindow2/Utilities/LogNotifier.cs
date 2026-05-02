using System.IO;

namespace DRDownloadWindow2.Utilities
{
    public class LogNotifier
    {
        private static readonly string LockId = Util.GenerateRandomGuid();

        private string? LogFile { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="logFile"></param>
        public LogNotifier(string? logFile)
        {
            LogFile = logFile;
        }

        /// <summary>
        /// Log with timestamp.
        /// </summary>
        /// <param name="line"></param>
        public void LogLine(string line)
        {
            if (string.IsNullOrEmpty(LogFile))
            { return; }

            lock (LockId)
            {
                File.AppendAllLines(LogFile, new string[] { $"{DateTime.Now:yyyy.MM.dd HH:mm:ss} {line}" }.AsEnumerable());
            }
        }

    }
    
}

