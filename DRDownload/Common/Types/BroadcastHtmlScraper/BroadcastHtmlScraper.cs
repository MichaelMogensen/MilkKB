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
            // TODO.

            /*
             
            Console.WriteLine("Title: " + InnerTextOfH2(mainBroadcastNode));
            Console.WriteLine("Description: " + InnerTextOfP(mainBroadcastNode));
            Console.WriteLine("Date: " + InnerTextOfDivHoldingSpanWithClassname(rightSideBroadcastNode, "info", "event"));
            Console.WriteLine("Duration: " + InnerTextOfDivHoldingSpanWithClassname(rightSideBroadcastNode, "info", "schedule"));
            Console.WriteLine("Channel: " + InnerTextOfDivHoldingSpanWithClassname(rightSideBroadcastNode, "info", "tv"));
            Console.WriteLine("Episode: " + InnerTextOfDivHoldingSpanWithClassname(rightSideBroadcastNode, "info", "segment"));
            Console.WriteLine("Genre: " + InnerTextOfLinkWithClassname(rightSideBroadcastNode, "genre-link"));
            Console.WriteLine("EntryId: " + EntryId(StyleOfDivWithClassname(mainBroadcastNode, "playkit-poster")));

            */
        }

        #region Helpers.

        /// <summary>
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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

