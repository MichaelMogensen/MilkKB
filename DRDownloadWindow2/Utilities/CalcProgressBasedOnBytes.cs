namespace DRDownloadWindow.Utilities
{
    public class CalcProgressBasedOnBytes
    {
        public int Value { get; private set; } = -1;

        private long TotalBytes { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public CalcProgressBasedOnBytes(long toalBytes)
        {
            TotalBytes = toalBytes;
        }

        /// <summary>
        /// Calc new progress.
        /// </summary>
        /// <param name="currentBytes"></param>
        /// <returns>T for new value otherwise F</returns>
        public bool Calc(long currentBytes)
        {
            var progress = Util.Percent(TotalBytes, currentBytes);
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

