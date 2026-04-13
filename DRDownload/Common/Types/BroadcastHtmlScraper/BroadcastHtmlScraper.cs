using DRDownload.Common.Types.BroadcastTypes;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace DRDownload.Common.Types.BroadcastHtmlScraper
{
    public class BroadcastHtmlScraper
    {
        public Broadcast? BroadcastRecord { get; private set; }

        private HtmlNode? MainBroadcastNode { get; set; }
        private HtmlNode? LeftSideBroadcastNode { get; set; }
        private HtmlNode? RightSideBroadcastNode { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="html"></param>
        public BroadcastHtmlScraper(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            MainBroadcastNode =
                htmlDocument.
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
            BroadcastRecord = new Broadcast
            {
                UniqueId = Util.GenerateRandomGuid(),
                MediaType = EMediaType.undesided,
                EntityId = EntryId(StyleOfDivWithClassname(MainBroadcastNode, "playkit-poster")),
                Title = InnerTextOfH2(MainBroadcastNode),
                Description = InnerTextOfP(MainBroadcastNode),
                SendDate = DateTime.MinValue,
                DurationMin = -1,
                Channel = InnerTextOfDivHoldingSpanWithClassname(RightSideBroadcastNode, "info", "tv"),
                Episode = InnerTextOfDivHoldingSpanWithClassname(RightSideBroadcastNode, "info", "segment"),
                Genre = InnerTextOfLinkWithClassname(RightSideBroadcastNode, "genre-link")
            };

            // TODO: Util.MediaFromChannel()
            // TODO: Util.DateFromNaturalDate(InnerTextOfDivHoldingSpanWithClassname(RightSideBroadcastNode, "info", "event"))
            // TODO: Util.DurationFromNaturalDuration(InnerTextOfDivHoldingSpanWithClassname(RightSideBroadcastNode, "info", "schedule"))
        }

        #region Helpers.

        /// <summary>
        /// Pick entryId from string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string? EntryId(string? text)
        {
            if (string.IsNullOrEmpty(text))
            { return null; }

            var pattern = @"entry_id/0_[a-z0-9]{8}";
            var regEx = new Regex(pattern);

            var firstMatch = regEx.Match(text);

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
        /// Read span ctrl.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private static string? InnerTextOfSpanWithClassname(HtmlNode? node, string className)
        {
            if (node == null)
            { return null; }

            var value =
                node.
                SelectSingleNode($"//span[@class=\"{className}\"]")?.
                InnerText?.
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

        /// <summary>
        /// Made to read out entryId string.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private static string? StyleOfDivWithClassname(HtmlNode? node, string className)
        {
            if (node == null)
            { return null; }

            var value =
                node.
                SelectSingleNode($"//div[@class=\"{className}\"]")?.
                GetAttributeValue("style", "style not found")?.
                Trim();

            return value;
        }

        #endregion

    }
}

