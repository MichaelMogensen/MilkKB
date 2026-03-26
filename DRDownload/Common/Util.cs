using System.Globalization;
using System.Text;

namespace DRDownload.Common
{
    public static class Util
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string GenerateRadomId(string prefix, int length, bool includeDigits, bool includeLetters)
        {
            var result = string.Empty;

            var digits = "0123456789";
            var letters = "abcdefghijklmnopqrstuvwxyz";

            var all = string.Empty;
            all += includeDigits ? digits : string.Empty;
            all += includeLetters ? letters : string.Empty;

            if (string.IsNullOrEmpty(all))
            { return result; }

            var rnd = new Random();
            while (result.Length < length)
            {
                var c = all[rnd.Next(0, all.Length)];
                result += c;
            }

            if (result[0] == '0')
            {
                return GenerateRadomId(prefix, length, includeDigits, includeLetters);
            }

            result = $"{prefix}{result}";
            return result;
        }

        public static string GenerateRandomGuid() =>
            Guid.NewGuid().ToString();

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

