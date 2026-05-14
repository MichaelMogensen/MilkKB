namespace DRDownloadLib.Utilities
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
            var progress = Util.Percent(TotalDuration.TotalMilliseconds, currentDuration.TotalMilliseconds);
            if (progress <= Value)
            {
                // No news.
                return false;
            }

            // Remember new progress.
            Value = progress;

            return true;
        }

    }
}

