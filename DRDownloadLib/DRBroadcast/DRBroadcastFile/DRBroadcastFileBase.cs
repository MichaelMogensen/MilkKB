using DRDownloadLib.Types;
using DRDownloadLib.Utilities;

namespace DRDownloadLib.DRBroadcast.DRBroadcastFile
{
    public abstract class DRBroadcastFileBase
    {
        private static readonly int MAX_FILE_LEN = 150;

        public string OutputFile { get; private set; } = string.Empty;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="broadcast"></param>
        public DRBroadcastFileBase(string ext, Broadcast broadcast)
        {
            CreateFilename(ext, broadcast);
        }

        /// <summary>
        /// Create resonable filename.
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="broadcast"></param>
        private void CreateFilename(string ext, Broadcast broadcast)
        {
            if (broadcast.DownloadFolder == null)
            {
                throw new ArgumentNullException($"{nameof(broadcast.DownloadFolder)} is null");
            }

            var timestamp = broadcast.SendDate?.ToString("yyyy.MM.dd.hh.mm");

            var filename =
                Util.EllipsisString(
                    Util.AggregateStringsNotNull(
                        ", ",
                        timestamp,
                        broadcast.Title,
                        Util.ToDanishDate(broadcast.SendDate),
                        Util.ToDanishDuration(broadcast.SendDate, broadcast.Duration),
                        broadcast.Channel,
                        broadcast.Episode,
                        broadcast.Description),
                    MAX_FILE_LEN).TrimEnd('.') + $".{ext ?? "unknown"}";

            filename = new ReplaceDisallowedFilenameCharacters(filename).Filename;
            filename = filename.Trim();

            OutputFile = Path.Combine(broadcast.DownloadFolder, filename);
            OutputFile = OutputFile.ToLower();
        }

    }
}


