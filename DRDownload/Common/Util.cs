using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Security.Cryptography;
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

        public static string DownloadFolder()
        {
            var folder = Environment.ExpandEnvironmentVariables("%userprofile%\\downloads");
            
            return folder;
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

        public static string Capitalized(string value)
        {
            if (string.IsNullOrEmpty(value) && value.Count() > 1)
            { return value; }

            var first = value.ToUpper().First().ToString();
            var rest = new string(value.ToLower().Skip(1).ToArray());

            var capitalized = first + rest;

            return capitalized;
        }

        public static string GenerateRandomGuid() =>
            Guid.NewGuid().ToString();

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

