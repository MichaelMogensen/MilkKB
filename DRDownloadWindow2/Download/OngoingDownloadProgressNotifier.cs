namespace DRDownloadWindow2.Download
{
    /// <summary>
    /// Notify progress based on duration and total duration. For console.
    /// </summary>
    public class OngoingDownloadProgressNotifier
    {
        private int Progress { get; set; } = -1;

        private TimeSpan TotalDuration { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public OngoingDownloadProgressNotifier(TimeSpan toalDuration)
        {
            TotalDuration = toalDuration;
        }

        /// <summary>
        /// Console helper for printing in same pos. Only refreshes on new progress, 
        /// like 34% -> 35%, otherwise bypass.
        /// </summary>
        /// <param name="currentDuration"></param>
        public void NotifyConsoleBelow100Pct(TimeSpan currentDuration)
        {
            var progress = ProgressAsInteger(currentDuration);
            if (progress == Progress)
            {
                // No news.
                return;
            }

            // Write new progress.
            Progress = progress;
            //DRMedia.PipeOutput?.PipeProgressTo(Progress);
            //DRMedia.PipeOutput?.PipeProgressTo(ProgressAsIntegerFmt());
        }

        /// <summary>
        /// Same as above. Called after progress ends to ensure we show 100%.
        /// </summary>
        public void NotifyConsoleAt100Pct()
        {
            NotifyConsoleBelow100Pct(TotalDuration);
            //DRMedia.PipeOutput?.PipeMessageTo();
        }

        /// <summary>
        /// Progress in % as double with a lot of decimals.
        /// </summary>
        /// <param name="currentDuration"></param>
        /// <returns></returns>
        private double ProgressAsDouble(TimeSpan currentDuration)
        {
            var totalDurationMS = TotalDuration.TotalMilliseconds;
            var currentDurationMS = currentDuration.TotalMilliseconds;

            var f = currentDurationMS / totalDurationMS;

            var pct = 100.0 * f;

            return pct;
        }

        /// <summary>
        /// Progress in % as int with NO decimals.
        /// </summary>
        /// <param name="currentDuration"></param>
        /// <returns></returns>
        private int ProgressAsInteger(TimeSpan currentDuration)
        {
            return (int)ProgressAsDouble(currentDuration);
        }

        /// <summary>
        /// Progress in % formatted.
        /// </summary>
        /// <returns></returns>
        private string ProgressAsIntegerFmt()
        {
            return $"{Progress}% downloaded";
        }

    }
}

