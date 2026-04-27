namespace DRDownloadWindow2.Utilities
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
            var progress = Progress(currentBytes);
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
        /// <param name="currentBytes"></param>
        /// <returns></returns>
        private int Progress(long currentBytes)
        {
            return Util.Percent((double)currentBytes / (double)TotalBytes);
        }

    }
}

