using DRDownload.Common.Types.BroadcastTypes;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace DRDownload.Common.Types.BroadcastHtmlScraper
{
    public class BroadcastHtmlScraper
    {
        public static readonly string BROADCAST_REC_DATA_PARENT_XPATH = "//div[@class=\"boardcast-record-data\"]";
        public static readonly string BROADCAST_REC_DATA_LEFT_XPATH = "//div[@class=\"main-record-data\"]";
        public static readonly string BROADCAST_REC_DATA_RIGHT_XPATH = "//div[@class=\"right-side\"]";

        public Broadcast? BroadcastRecord { get; private set; }

        public HtmlDocument Document { get; set; } = new HtmlDocument();

        private HtmlNode? MainBroadcastNode { get; set; }
        private HtmlNode? LeftSideBroadcastNode { get; set; }
        private HtmlNode? RightSideBroadcastNode { get; set; }

        /// <summary>
        /// RegEx pattern differ a little depending on media.
        /// </summary>
        /// <param name="medieType"></param>
        /// <returns></returns>
        private string EntryIdPattern(EMediaType medieType) =>
            medieType == EMediaType.radio ?
            @"entryId/0_[a-z0-9]{8}" :
            @"entry_id/0_[a-z0-9]{8}";

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="html"></param>
        public BroadcastHtmlScraper(string url, string html)
        {
            Document.LoadHtml(html);

            MainBroadcastNode =
                Document.
                DocumentNode?.
                SelectSingleNode(BROADCAST_REC_DATA_PARENT_XPATH);

            LeftSideBroadcastNode =
                MainBroadcastNode?.
                SelectSingleNode(BROADCAST_REC_DATA_LEFT_XPATH);

            RightSideBroadcastNode =
                MainBroadcastNode?.
                SelectSingleNode(BROADCAST_REC_DATA_RIGHT_XPATH);

            CreateBroadcastRecord(url);
        }

        /// <summary>
        /// Try to lookup entryId from string.
        /// </summary>
        /// <returns></returns>
        private string? LookupEntryId(EMediaType medieType)
        {
            var regEx = new Regex(EntryIdPattern(medieType));
            var firstMatch = regEx.Match(Document.ParsedText);

            if (string.IsNullOrEmpty(firstMatch?.Value))
            { return null; }

            var entryId = firstMatch.Value.Trim().Split('/')?.Last();

            return entryId;
        }

        /// <summary>
        /// Create broadcast record.
        /// </summary>
        /// <param name="url"></param>
        private void CreateBroadcastRecord(string url)
        {
            // Parts for parts.
            var drEvent = new ParseDRDate(InnerTextOfDivHoldingSpanWithClassname(RightSideBroadcastNode, "info", "event")).Date;
            var drSchedule = new ParseDRDuration(InnerTextOfDivHoldingSpanWithClassname(RightSideBroadcastNode, "info", "schedule"));

            // Parts.
            var uniqueId = Util.GenerateRandomGuid();
            var medieType = url.Contains("radio") ? EMediaType.radio : EMediaType.tv;
            var entryId = LookupEntryId(medieType);
            var channel = InnerTextOfDivHoldingSpanWithClassname(RightSideBroadcastNode, "info", "tv");
            var title = InnerTextOfH2(MainBroadcastNode);
            var description = InnerTextOfP(MainBroadcastNode);
            var sendDate = new DateTime(
                drEvent.Date.Year,
                drEvent.Date.Month,
                drEvent.Date.Day,
                drSchedule.From.Hour,
                drSchedule.From.Minute,
                drSchedule.From.Second);
            var durationMin = (int)drSchedule.Duration.TotalMinutes;
            var episode = InnerTextOfDivHoldingSpanWithClassname(RightSideBroadcastNode, "info", "segment");
            var genre = InnerTextOfLinkWithClassname(RightSideBroadcastNode, "genre-link");

            // Full object.
            BroadcastRecord = new Broadcast
            {
                UniqueId = uniqueId,
                EntryId = entryId,
                Channel = channel,
                Title = title,
                Description = description,
                SendDate = sendDate,
                DurationMin = durationMin,
                Episode = episode,
                Genre = genre,
                MediaType = medieType,
                KbUrl = url
            };
        }

        #region Html parsing helpers.

        /// <summary>
        /// Read h2 ctrl.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="default_"></param>
        /// <returns></returns>
        private static string? InnerTextOfH2(HtmlNode? node, string? default_ = null)
        {
            if (node == null)
            { return null; }

            var value = node.SelectSingleNode("//h2").InnerText.Trim();

            return value ?? default_;
        }

        /// <summary>
        /// Read p ctrl.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="default_"></param>
        /// <returns></returns>
        private static string? InnerTextOfP(HtmlNode? node, string? default_ = null)
        {
            if (node == null)
            { return null; }

            var value = node.SelectSingleNode("//p").InnerText.Trim();

            return value ?? default_;
        }

        /// <summary>
        /// Read div ctrl holding span ctrl. When reading inner text we must remove inner 
        /// text of div to get inner text og span because they are automatically added.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="className"></param>
        /// <param name="leadDivInnerTextToRemove"></param>
        /// <param name="default_"></param>
        /// <returns></returns>
        private static string? InnerTextOfDivHoldingSpanWithClassname(HtmlNode? node, string className, string leadDivInnerTextToRemove, string? default_ = null)
        {
            if (node == null)
            { return null; }

            var value =
                node.
                SelectNodes($"//div[@class=\"{className}\"]").
                Where(node => node.InnerText.StartsWith(leadDivInnerTextToRemove))?.
                FirstOrDefault()?.
                InnerText.
                TrimStart(leadDivInnerTextToRemove.ToCharArray())?.
                Trim();

            return value ?? default_;
        }

        /// <summary>
        /// Read a ctrl.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="className"></param>
        /// <param name="default_"></param>
        /// <returns></returns>
        private static string? InnerTextOfLinkWithClassname(HtmlNode? node, string className, string? default_ = null)
        {
            if (node == null)
            { return null; }

            var value =
                node.
                SelectSingleNode($"//a[@class=\"{className}\"]")?.
                InnerText?.
                Trim();

            return value ?? default_;
        }

        #endregion

    }
}

