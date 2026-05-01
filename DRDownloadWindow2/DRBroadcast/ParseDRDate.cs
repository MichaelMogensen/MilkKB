using System.Globalization;

namespace DRDownloadWindow2.DRBroadcast
{
    /// <summary>
    /// Parse complex time string. like "20. maj 2017" into time objects.
    /// </summary>
    public class ParseDRDate
    {
        public DateTime Date { get; private set; } = DateTime.MinValue;

        /// <summary>
        /// Ctor.
        /// </summary>
        public ParseDRDate(string? dateExpression)
        {
            Parse(dateExpression);
        }

        /// <summary>
        /// Parse complex date string.
        /// </summary>
        /// <param name="dateExpression"></param>
        private void Parse(string? dateExpression)
        {
            if (string.IsNullOrEmpty(dateExpression))
            { return; }

            try
            {
                var danish = new CultureInfo("da-DK");
                Date = DateTime.Parse(dateExpression, danish);
            }
            catch
            {
                // Bypass parsing error.
            }
        }
    }
}

