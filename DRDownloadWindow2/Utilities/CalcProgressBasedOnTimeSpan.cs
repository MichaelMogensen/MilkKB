namespace DRDownloadWindow2.Utilities
{
    public class CalcProgressBasedOnTimeSpan
    {
        public int Value { get; private set; } = -1;

        private TimeSpan TotalDuration { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public CalcProgressBasedOnTimeSpan(TimeSpan toalDuration)
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
            var progress = Progress(currentDuration);
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
        /// Progress in % as int with NO decimals.
        /// </summary>
        /// <param name="currentDuration"></param>
        /// <returns></returns>
        private int Progress(TimeSpan currentDuration)
        {
            var totalDurationMS = TotalDuration.TotalMilliseconds;
            var currentDurationMS = currentDuration.TotalMilliseconds;

            return Util.Percent(currentDurationMS / totalDurationMS);
        }

    }
}

