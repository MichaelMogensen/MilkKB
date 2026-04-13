using DRDownload.Common.Types.BroadcastTypes;
using HtmlAgilityPack;
using OpenQA.Selenium.DevTools.V145.Debugger;
using System.Text.RegularExpressions;

namespace DRDownload.Common.Types.BroadcastHtmlScraper
{
    public class BroadcastHtmlScraper
    {
        public Broadcast? BroadcastRecord { get; private set; }

        public HtmlDocument Document { get; set; } = new HtmlDocument();

        private HtmlNode? MainBroadcastNode { get; set; }
        private HtmlNode? LeftSideBroadcastNode { get; set; }
        private HtmlNode? RightSideBroadcastNode { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="html"></param>
        public BroadcastHtmlScraper(string html)
        {
            Document.LoadHtml(html);

            MainBroadcastNode =
                Document.
                DocumentNode?.
                SelectSingleNode("//div[@class=\"boardcast-record-data\"]");

            LeftSideBroadcastNode =
                MainBroadcastNode?.
                SelectSingleNode("//div[@class=\"main-record-data\"]");

            RightSideBroadcastNode =
                MainBroadcastNode?.
                SelectSingleNode("//div[@class=\"right-side\"]");

            ScrapeBroadcastRecord();
        }

        /// <summary>
        /// Establish record.
        /// </summary>
        private void ScrapeBroadcastRecord()
        {
            // Parts for parts.
            var drEvent = new ParseDRDate(InnerTextOfDivHoldingSpanWithClassname(RightSideBroadcastNode, "info", "event")).Date;
            var drSchedule = new ParseDRDuration(InnerTextOfDivHoldingSpanWithClassname(RightSideBroadcastNode, "info", "schedule"));

            // Parts.
            var uniqueId = Util.GenerateRandomGuid();
            var entityId = ParseEntryId();
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
            var medieType = string.IsNullOrEmpty(channel) ? EMediaType.undesided : channel.StartsWith("P") ? EMediaType.radio : EMediaType.tv;

            // Full object.
            BroadcastRecord = new Broadcast
            {
                UniqueId = uniqueId,
                EntityId = entityId,
                Channel = channel,
                Title = title,
                Description = description,
                SendDate = sendDate,
                DurationMin = durationMin,
                Episode = episode,
                Genre = genre,
                MediaType = medieType
            };
        }

        #region Html parsing helpers.

        /// <summary>
        /// Parse entryId from string.
        /// </summary>
        /// <returns></returns>
        private string? ParseEntryId()
        {

            var pattern = @"entry_id/0_[a-z0-9]{8}";
            var regEx = new Regex(pattern);

            var firstMatch = regEx.Match(Document.ParsedText);

            if (string.IsNullOrEmpty(firstMatch?.Value))
            { return null; }

            var entryId = firstMatch.Value.Trim().Split('/')?.Last();

            return entryId;
        }

        /// <summary>
        /// Read h2 ctrl.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string? InnerTextOfH2(HtmlNode? node)
        {
            if (node == null)
            { return null; }

            var value = node.SelectSingleNode("//h2").InnerText;

            return value;
        }

        /// <summary>
        /// Read p ctrl.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string? InnerTextOfP(HtmlNode? node)
        {
            if (node == null)
            { return null; }

            var value = node.SelectSingleNode("//p").InnerText;

            return value;
        }

        /// <summary>
        /// Read div ctrl holding span ctrl. When reading inner text we must remove inner 
        /// text of div to get inner text og span because they are automatically added.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="className"></param>
        /// <param name="leadDivInnerTextToRemove"></param>
        /// <returns></returns>
        private static string? InnerTextOfDivHoldingSpanWithClassname(HtmlNode? node, string className, string leadDivInnerTextToRemove)
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

            return value;
        }

        /// <summary>
        /// Read a ctrl.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private static string? InnerTextOfLinkWithClassname(HtmlNode? node, string className)
        {
            if (node == null)
            { return null; }

            var value =
                node.
                SelectSingleNode($"//a[@class=\"{className}\"]")?.
                InnerText?.
                Trim();

            return value;
        }

        #endregion

    }
}

