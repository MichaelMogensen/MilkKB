using HtmlAgilityPack;

namespace DRDownloadLib.Utilities
{
    public class FluentLocateStringByNode
    {
        private HtmlNode? Node { get; set; }
        public string? Result { get; private set; }

        public FluentLocateStringByNode(HtmlNode? node)
        {
            Node = node;
        }

        public FluentLocateStringByNode TryLocate(string xpath)
        {
            InnerText(xpath);

            return this;
        }

        public FluentLocateStringByNode OrTryLocate(string xpath)
        {
            if (Result == null)
            {
                InnerText(xpath);
            }

            return this;
        }

        public FluentLocateStringByNode OrDefault(string? default_ = null)
        {
            if (Result == null)
            {
                Result = default_;
            }

            return this;
        }

        private void InnerText(string xpath)
        {
            Result = Node?.SelectSingleNode(xpath).InnerText.Trim();
        }

    }
}

