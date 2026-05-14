using System.Text.RegularExpressions;

namespace DRDownloadLib.DRBroadcast
{
    /// <summary>
    /// Parse complex time string. like "kl. 14:40 - 15:09 ( 29min 31sek )" into time objects.
    /// </summary>
    public class ParseDRDuration
    {
        public TimeOnly From { get; private set; }
        public TimeOnly To { get; private set; }
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="timeExpression"></param>
        public ParseDRDuration(string? timeExpression)
        {
            Parse(timeExpression);
        }

        /// <summary>
        /// Parse complex time string.
        /// </summary>
        /// <param name="timeExpression"></param>
        private void Parse(string? timeExpression)
        {
            if (string.IsNullOrEmpty(timeExpression))
            { return; }

            try
            {
                string pattern = @"\d\d:\d\d";
                var regEx = new Regex(pattern);
                var matches = regEx.Matches(timeExpression);

                var from = matches[0].Value;
                var to = matches[1].Value;

                From = TimeOnlyByString(from);
                To = TimeOnlyByString(to);

                Duration = To - From;
            }
            catch
            {
                // Bypass parsing error.
            }
        }

        /// <summary>
        /// Parse string like "14:40" into object.
        /// </summary>
        /// <param name="hhmmTime"></param>
        /// <returns></returns>
        private static TimeOnly TimeOnlyByString(string? hhmmTime)
        {
            if (string.IsNullOrEmpty(hhmmTime))
            { return new TimeOnly(); }

            try
            {
                var timeParts = hhmmTime.Trim().Split(":");

                var hours = int.Parse(timeParts[0]);
                var minutes = int.Parse(timeParts[1]);

                return new TimeOnly(hours, minutes, 0);
            }
            catch
            {
                // Bypass parsing error.
            }

            return new TimeOnly();
        }
    }
}

