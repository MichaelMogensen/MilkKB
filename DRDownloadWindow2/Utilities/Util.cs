using DRDownloadWindow2.Types;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace DRDownloadWindow2.Utilities
{
    public static class Util
    {
        /// <summary>
        /// Aggregate N smaller not-null strings into one big string.
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static string AggregateStringsNotNull(string separator, params string?[] parts)
        {
            var notNullParts =
                parts.
                Where(part => part is not null).
                Aggregate((p1, p2) => $"{p1}{separator}{p2}");

            return notNullParts ?? string.Empty;
        }

        /// <summary>
        /// Base64.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// First upper and rest lower.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CapitalizedString(string value)
        {
            if (string.IsNullOrEmpty(value) && value.Count() > 1)
            { return value; }

            var first = value.ToUpper().First().ToString();
            var rest = new string(value.ToLower().Skip(1).ToArray());

            var capitalized = first + rest;

            return capitalized;
        }

        /// <summary>
        /// Makes too long string a shorter string ending with ...
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <param name="endingOnTooLong"></param>
        /// <returns></returns>
        public static string EllipsisString(string value, int maxLength, string endingOnTooLong = "...")
        {
            if (value.Length > maxLength)
            {
                var shortValue = $"{value.Substring(0, maxLength)} {endingOnTooLong}";

                return shortValue;
            }

            return value;
        }

        /// <summary>
        /// Random guid as text.
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomGuid() =>
            Guid.NewGuid().ToString();

        /// <summary>
        /// Random id at specific length.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="length"></param>
        /// <param name="includeDigits"></param>
        /// <param name="includeLetters"></param>
        /// <returns></returns>
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

        /// <summary>
        /// DK date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string? ToDanishDate(DateTime? date) =>
            date == null ? null : date.Value.ToString("d. MMMM yyyy", new CultureInfo("da-DK"));

        /// <summary>
        /// DK duration.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static string? ToDanishDuration(DateTime? date, TimeSpan? duration)
        {
            if (date == null || duration == null)
            {
                return null;
            }

            var from = date;
            var to = date + duration;

            var from_ = $"{from.Value.Hour:00}.{from.Value.Minute:00}";
            var to_ = $"{to.Value.Hour:00}.{to.Value.Minute:00}";

            var period_ = $"fra {from_} til {to_}";

            return period_;
        }

        public static string? ToDanishDateAndDuration(DateTime? date, TimeSpan? duration)
        {
            if (date == null || duration == null)
            {
                return null;
            }

            var date_ = ToDanishDate(date);
            var period_ = ToDanishDuration(date, duration);
            var totalMinutes_ = duration.Value.TotalMinutes;

            return $"{date_} {period_} ({totalMinutes_} min)";
        }

        /// <summary>
        /// Establish download folder.
        /// </summary>
        /// <returns></returns>
        public static string WindowsDownloadFolder()
        {
            var folder = Environment.ExpandEnvironmentVariables("%userprofile%\\downloads").ToLower();

            return folder;
        }

        /// <summary>
        /// Color (for binding) by warning level.
        /// </summary>
        /// <param name="warningLevel"></param>
        /// <returns></returns>
        public static string WarningLevelToColor(EWarningLevel warningLevel)
        {
            return warningLevel switch
            {
                EWarningLevel.error => Color.Red.Name.ToString(),
                EWarningLevel.warning => Color.Orange.Name.ToString(),
                _ => Color.Gray.Name.ToString()
            };
        }

        /// <summary>
        /// Write in fixed pos. in console.
        /// </summary>
        /// <param name="text"></param>
        public static void WriteInFixedConsolePosition(string text)
        {
            var top = Console.GetCursorPosition().Top;
            Console.Write(text);
            Console.SetCursorPosition(0, top);
        }
    }
}

