using System.Text.RegularExpressions;

namespace DRDownloadLib.Utilities
{
    public class FluentLocateStringByRegEx
    {
        private string? Text { get; set; }
        public string? Result { get; private set; }

        public FluentLocateStringByRegEx(string? text)
        {
            Text = text;
        }

        public FluentLocateStringByRegEx TryLocate(string pattern)
        {
            Result = TryLookup(pattern);

            return this;
        }

        public FluentLocateStringByRegEx TryLocateMany(string pattern)
        {
            Result = TryLookupMany(pattern);

            return this;
        }

        public FluentLocateStringByRegEx OrTryLocate(string pattern)
        {
            if (string.IsNullOrEmpty(Result))
            {
                Result = TryLookup(pattern);
            }

            return this;
        }

        public FluentLocateStringByRegEx OrDefault(string? default_ = null)
        {
            if (string.IsNullOrEmpty(Result))
            {
                Result = default_;
            }

            return this;
        }

        private string? TryLookup(string pattern)
        {
            if (string.IsNullOrEmpty(Text))
            { return null; }

            var regEx = new Regex(pattern);
            var firstMatch = regEx.Match(Text);

            return firstMatch?.Value;
        }

        private string? TryLookupMany(string pattern)
        {
            if (string.IsNullOrEmpty(Text))
            { return null; }

            var regEx = new Regex(pattern);

            var allMatches = new List<string>();
            var m = regEx.Matches(Text);

            for (var id = 0; id < m.Count; id++)
            {
                allMatches.Add(m[id].Value);
            }

            var matches = allMatches.Count > 0 ? allMatches.Aggregate((i1, i2) => $"{i1}|{i2}") : null;

            return matches;
        }
    }
}

