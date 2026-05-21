using DRDownloadLib.Types;
using DRDownloadLib.Utilities;
using System.IO;

namespace DRDownloadWindow.DRBroadcast.DRBroadcastFile
{
    public abstract class DRBroadcastFileBase
    {
        private static readonly string SORTABLE_TS = "yyyy.MM.dd.";

        private Broadcast Broadcast { get; set; }
        private string Ext { get; set; }

        private string BasePath =>
            Broadcast.DownloadFolder ?? Util.WindowsTempFolder();

        private string Filename =>
            new FilesystemSafe($"{SortableTimestamp} {Broadcast.TitleAndEpisode}.{Ext}").File;

        /// <summary>
        /// Create simple sortable TS.
        /// </summary>
        private string? SortableTimestamp => 
            Broadcast.SendDate?.ToString(SORTABLE_TS);

        /// <summary>
        /// Create resonable filename, like "1990.05.08. kunstquiz 3 af 6.mp4".
        /// </summary>
        public string OutputFile =>
            new UniqueFilenameInFolder(Path.Combine(BasePath, Filename)).File;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="broadcast"></param>
        public DRBroadcastFileBase(string ext, Broadcast broadcast)
        {
            Ext = ext;
            Broadcast = broadcast;
        }
    }
}

