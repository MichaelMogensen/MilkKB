using DRDownloadWindow.Extensions;
using DRDownloadWindow.Utilities;

namespace DRDownloadWindow.DRBroadcast
{
    public class DRBroadcastInfoByRegEx
    {
        public string? Text { get; private set; }

        public DRBroadcastInfoByRegEx(string? text)
        {
            Text = text;
        }

        public string? GetSendDate()
        {
            return
                new FluentLocateStringByRegEx(Text).
                TryLocate(Const.REGEX_PATTERN_SEND_DATE).Result;
        }

        public string? GetSendHours()
        {
            return
                new FluentLocateStringByRegEx(Text).
                TryLocate(Const.REGEX_PATTERN_SEND_HOURS).Result;
        }

        public string? GetChannel()
        {
            return
                new FluentLocateStringByRegEx(Text).
                TryLocate(Const.REGEX_PATTERN_CHANNEL).Result?.TrimTagStart()?.Trim();
        }

        public string? GetEpisode()
        {
            Func<string[]?, int, int> Clean = (arr, id) =>
            {
                if (arr != null && arr.Count() >= id + 1)
                {
                    var s = arr[id].TrimTagEnd()?.Trim();
                    if (!string.IsNullOrEmpty(s))
                    {
                        var no = int.Parse(s);
                        return no;
                    }
                }

                return 0;
            };

            var episodeInfo = new FluentLocateStringByRegEx(Text).TryLocateMany(Const.REGEX_PATTERN_EPISODE).Result;
            var episodeInfoSplit = episodeInfo?.Split('|');

            var a = Clean(episodeInfoSplit, 0);
            var b = Clean(episodeInfoSplit, 1);

            var min = Math.Min(a, b);
            var max = Math.Max(a, b);


            if (min == 0 && max > 0)
            {
                return $"Del {max}";
            }
            else if (min > 0 && max > 0)
            {
                return $"Del {min} af {max}";
            }

            return null;
        }

        public string? GetGenre()
        {
            return
                new FluentLocateStringByRegEx(Text).
                TryLocate(Const.REGEX_PATTERN_GENRE).Result?.TrimTagEnd()?.Trim();
        }

        public string? GetEntryId()
        {
            var entryId =
                new FluentLocateStringByRegEx(Text).
                TryLocate($"entryId/{Const.REGEX_PATTERN_ENTRY_ID}").
                OrTryLocate($"entry_id/{Const.REGEX_PATTERN_ENTRY_ID}").
                Result;

            if (!string.IsNullOrEmpty(entryId))
            {
                if (entryId.Contains('/'))
                {
                    entryId = entryId.Split('/')[1];

                    return entryId;
                }
            }

            return null;
        }
    }
}

