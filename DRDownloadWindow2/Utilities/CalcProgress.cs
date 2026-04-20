namespace DRDownloadWindow2.Utilities
{
    public class CalcProgress
    {
        public int Value { get; private set; } = -1;

        private TimeSpan TotalDuration { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public CalcProgress(TimeSpan toalDuration)
        {
            TotalDuration = toalDuration;
        }

        /// <summary>
        /// Calc new progress.
        /// </summary>
        /// <param name="currentDuration"></param>
        /// <returns>T for new value otherwise F</returns>
        public bool Calc(TimeSpan currentDuration)
        {
            var progress = ProgressAsInteger(currentDuration);
            if (progress <= Value)
            {
                // No news.
                return false;
            }

            // Remember new progress.
            Value = progress;

            return true;
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

            pct = Math.Max(0.0, pct);
            pct = Math.Min(100.0, pct);

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

    }
}

