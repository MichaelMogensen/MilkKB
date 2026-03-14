using System.Globalization;
using System.Text;

namespace MilkKB.util
{
    public static class Util
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        public static string ToDanishDate(DateTime date) =>
            date.ToString("d. MMMM yyyy", new CultureInfo("da-DK"));

        public static string ToDanishDuration(DateTime sendDate, TimeSpan duration)
        {
            var from = sendDate;
            var to = sendDate + duration;

            var from_ = $"{from.Hour:00}.{from.Minute:00}";
            var to_ = $"{to.Hour:00}.{to.Minute:00}";
            
            var period_ = $"{from_} - {to_}";

            return period_;
        }
    }
}

