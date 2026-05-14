namespace DRDownloadLib
{
    public static class Const
    {
        public static readonly string REGEX_PATTERN_ENTRY_ID = "0_[a-z0-9]{8}";

        public static readonly string REGEX_PATTERN_SEND_DATE = @" \d*\d. \w+ \d\d\d\d";
        public static readonly string REGEX_PATTERN_SEND_HOURS = @"\d\d:\d\d - \d\d:\d\d";
        public static readonly string REGEX_PATTERN_CHANNEL = @"(> DR \w|> DR\d|> P\d)";
        public static readonly string REGEX_PATTERN_EPISODE = @"\d+<\/span>";
        public static readonly string REGEX_PATTERN_GENRE = @"(\w| |,|\.)+<\/a>";
    }
}

//public static readonly string REGEX_PATTERN_TITLE = "<h1 (\\s|\\S)+<\\/h1>";

