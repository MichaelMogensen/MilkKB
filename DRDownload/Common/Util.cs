using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text;

namespace DRDownload.Common
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
        /// Read JSON file and return instance of object it should be able to deserialize to.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public static T? DeserializeFile<T>(string file)
        {
            try
            {
                var contents = File.ReadAllText(file);
                JObject.Parse(contents);
                var desObject = JsonConvert.DeserializeObject<T>(contents);

                return desObject;
            }
            catch { }

            return default;
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
        /// Ensure to write "null" on object == null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theObject"></param>
        /// <returns></returns>
        public static string? OrNull<T>(T theObject) =>
            theObject is null ? "null" : theObject.ToString();

        /// <summary>
        /// DK date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDanishDate(DateTime date) =>
            date.ToString("d. MMMM yyyy", new CultureInfo("da-DK"));

        /// <summary>
        /// DK duration.
        /// </summary>
        /// <param name="sendDate"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static string ToDanishDuration(DateTime sendDate, TimeSpan duration)
        {
            var from = sendDate;
            var to = sendDate + duration;

            var from_ = $"{from.Hour:00}.{from.Minute:00}";
            var to_ = $"{to.Hour:00}.{to.Minute:00}";

            var period_ = $"{from_} - {to_}";

            return period_;
        }

        /// <summary>
        /// Establish download folder.
        /// </summary>
        /// <returns></returns>
        public static string WindowsDownloadFolder()
        {
            var folder = Environment.ExpandEnvironmentVariables("%userprofile%\\downloads");

            return folder;
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

