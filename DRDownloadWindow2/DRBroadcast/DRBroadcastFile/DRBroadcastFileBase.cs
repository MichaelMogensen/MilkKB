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

        /// <summary>
        /// Create simple sortable TS.
        /// </summary>
        private string? SortableTimestamp => 
            Broadcast.SendDate?.ToString(SORTABLE_TS);

        /// <summary>
        /// Create resonable filename like "1990.05.08. kunstquiz 3 af 6.mp4".
        /// </summary>
        public string OutputFile =>
            Path.Combine(
                Broadcast.DownloadFolder ?? Util.WindowsTempFolder(), 
                $"{SortableTimestamp} {Broadcast.TitleAndEpisode}.{Ext}");

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

