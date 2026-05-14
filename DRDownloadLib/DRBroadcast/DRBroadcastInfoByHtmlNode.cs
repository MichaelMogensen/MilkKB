using DRDownloadLib.Utilities;
using HtmlAgilityPack;

namespace DRDownloadLib.DRBroadcast
{
    public class DRBroadcastInfoByHtmlNode
    {
        public HtmlNode? Node { get; private set; }

        public DRBroadcastInfoByHtmlNode(HtmlNode? node)
        {
            Node = node;
        }

        public string? GetTitle()
        {
            return
                new FluentLocateStringByNode(Node).
                    TryLocate("//h1").
                    OrTryLocate("//h2").Result;
        }

        public string? GetDescription()
        {
            return
                new FluentLocateStringByNode(Node).
                    TryLocate("//p").Result;
        }

    }
}

