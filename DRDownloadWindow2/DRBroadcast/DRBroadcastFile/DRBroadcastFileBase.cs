using DRDownloadWindow2.Types;
using DRDownloadWindow2.Utilities;
using System.IO;

namespace DRDownloadWindow2.DRBroadcast.DRBroadcastFile
{
    public abstract class DRBroadcastFileBase
    {
        private static readonly int MAX_FILE_LEN = 200;

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

            var filename =
                Util.EllipsisString(
                    Util.AggregateStringsNotNull(
                        ", ",
                        broadcast.Title,
                        Util.ToDanishDate(broadcast.SendDate),
                        Util.ToDanishDuration(broadcast.SendDate, broadcast.Duration),
                        broadcast.Channel,
                        broadcast.Episode,
                        broadcast.Description),
                    MAX_FILE_LEN).TrimEnd('.') + $".{ext ?? "unknown"}";

            filename = new ReplaceDisallowedFilenameCharacters(filename).Filename;

            OutputFile = Path.Combine(broadcast.DownloadFolder, filename);
            OutputFile = OutputFile.ToLower();
        }

    }
}


