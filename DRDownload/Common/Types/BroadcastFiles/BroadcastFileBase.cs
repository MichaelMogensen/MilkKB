using DRDownload.Common.Types.BroadcastTypes;

namespace DRDownload.Common.Types.BroadcastFiles
{
    public abstract class BroadcastFileBase
    {
        private static readonly int MAX_FILE_LEN = 200;

        public string OutputFile { get; private set; } = string.Empty;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="ext"></param>
        /// <param name="broadcast"></param>
        public BroadcastFileBase(string basePath, string ext, Broadcast broadcast)
        {
            CreateFilename(basePath, ext, broadcast);
        }

        /// <summary>
        /// Create resonable filename.
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="ext"></param>
        /// <param name="broadcast"></param>
        private void CreateFilename(string basePath, string ext, Broadcast broadcast)
        {
            var filename = 
                $"{broadcast.Title}, " +
                $"{Util.ToDanishDate(broadcast.SendDate)}, " +
                $"{Util.ToDanishDuration(broadcast.SendDate, broadcast.Duration)} på " +
                $"{broadcast.Channel}, " +
                $"{broadcast.Episode}, " +
                $"{broadcast.Description}";

            if (filename.Length > MAX_FILE_LEN)
            {
                filename = filename.Substring(0, MAX_FILE_LEN) + " [...forkortet]";
            }

            filename += $".{ext}";
            filename = filename.Trim(":#%&{}\\<>".ToCharArray());
            filename = filename.Replace("/", " af ");

            OutputFile = Path.Combine(basePath, filename);
            OutputFile = OutputFile.ToLower();
        }
    }
}

