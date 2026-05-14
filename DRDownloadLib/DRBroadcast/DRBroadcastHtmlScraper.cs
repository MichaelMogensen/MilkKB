using DRDownloadLib.DRBroadcast.DRBroadcastFile;
using DRDownloadLib.Types;
using DRDownloadLib.Utilities;
using HtmlAgilityPack;

namespace DRDownloadLib.DRBroadcast
{
    public class DRBroadcastHtmlScraper
    {
        public static readonly string BROADCAST_REC_DATA_PARENT_XPATH = "//div[@class=\"boardcast-record-data\"]";

        public Broadcast Broadcast { get; private set; } = new Broadcast() { MediaType = EMediaType.nomedia };

        public HtmlDocument Document { get; set; } = new HtmlDocument();

        private HtmlNode? MainBroadcastNode { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="html"></param>
        public DRBroadcastHtmlScraper(string? url, string? html)
        {
            if (string.IsNullOrEmpty(url))
            { return; }
            Broadcast.Url = url;

            if (string.IsNullOrEmpty(html))
            { return; }
            Document.LoadHtml(html);

            Broadcast.DownloadFolder = Util.WindowsDownloadFolder();
            if (string.IsNullOrEmpty(Broadcast.DownloadFolder))
            {

            }

            CreateBroadcastRecord();
        }

        /// <summary>
        /// Create broadcast record.
        /// </summary>
        private void CreateBroadcastRecord()
        {
            try
            {
                var mediaType = MediaTypeByUrl(Broadcast.Url);
                if (mediaType == EMediaType.nomedia)
                {
                    throw new DRBroadcastHtmlException("Can't find media type");
                }

                var broadcastRaw = ParseHtml(); // Critical to survive this call.

                var sendDate_ = new ParseDRDate(broadcastRaw.SEND_DATE).Date;
                var sendHours_ = new ParseDRDuration(broadcastRaw.SEND_HOURS);

                // Set full Broadcast object. Broadcast.Url and Broadcast.DownloadFolder is already set.
                Broadcast.UniqueId = Util.GenerateRandomGuid();
                Broadcast.EntryId = broadcastRaw.ENTRYID;
                Broadcast.Channel = broadcastRaw.CHANNEL;
                Broadcast.Title = broadcastRaw.TITLE;
                Broadcast.Description = broadcastRaw.TITLE;
                Broadcast.SendDate = new DateTime(
                    sendDate_.Date.Year,
                    sendDate_.Date.Month,
                    sendDate_.Date.Day,
                    sendHours_.From.Hour,
                    sendHours_.From.Minute,
                    sendHours_.From.Second);
                Broadcast.DurationMin = (int)sendHours_.Duration.TotalMinutes;
                Broadcast.Episode = broadcastRaw.EPISODE;
                Broadcast.Genre = broadcastRaw.GENRE;
                Broadcast.MediaType = mediaType;

                Broadcast.Mp3File = null;
                Broadcast.M3uFile = null;
                Broadcast.Mp4File = null;
                Broadcast.LogFile = null;

                if (mediaType == EMediaType.radio)
                {
                    Broadcast.Mp3File = new DRMP3BroadcastFile(Broadcast).OutputFile;
                    Broadcast.M3uFile = null;
                    Broadcast.Mp4File = null;
                    Broadcast.LogFile = Path.ChangeExtension(Broadcast.Mp3File, "log");
                }
                else if (mediaType == EMediaType.tv)
                {
                    Broadcast.Mp3File = null;
                    Broadcast.M3uFile = new DRM3U8BroadcastFile(Broadcast).OutputFile;
                    Broadcast.Mp4File = Path.ChangeExtension(Broadcast.M3uFile, "mp4");
                    Broadcast.LogFile = Path.ChangeExtension(Broadcast.M3uFile, "log");
                }

            }
            catch (DRBroadcastHtmlException)
            {
            }
        }

        private BroadcastRaw ParseHtml()
        {
            var broadcastRaw = new BroadcastRaw();

            MainBroadcastNode =
                Document.
                DocumentNode?.
                SelectSingleNode(BROADCAST_REC_DATA_PARENT_XPATH);
            if (MainBroadcastNode == null)
            {
                throw new DRBroadcastHtmlException("Can't find main broadcast node");
            }

            var broadcastNodeInfoByHtmlNode = new DRBroadcastInfoByHtmlNode(MainBroadcastNode);
            var documentNodeInfoByRegEx = new DRBroadcastInfoByRegEx(Document.DocumentNode?.InnerHtml);
            var broadcastNodeInfoByRegEx = new DRBroadcastInfoByRegEx(MainBroadcastNode.InnerHtml);

            broadcastRaw.ENTRYID = documentNodeInfoByRegEx.GetEntryId();

            broadcastRaw.TITLE = broadcastNodeInfoByHtmlNode.GetTitle();
            if (string.IsNullOrEmpty(broadcastRaw.TITLE))
            {
                broadcastRaw.TITLE = "Ingen titel";
            }

            broadcastRaw.DESCRIPTION = broadcastNodeInfoByHtmlNode.GetDescription();
            if (string.IsNullOrEmpty(broadcastRaw.DESCRIPTION))
            {
                broadcastRaw.DESCRIPTION = "Ingen beskrivelse";
            }

            broadcastRaw.SEND_DATE = broadcastNodeInfoByRegEx.GetSendDate();
            if (string.IsNullOrEmpty(broadcastRaw.SEND_DATE))
            {
                broadcastRaw.SEND_DATE = "1. Januar 1970";
            }

            broadcastRaw.SEND_HOURS = broadcastNodeInfoByRegEx.GetSendHours();
            if (string.IsNullOrEmpty(broadcastRaw.SEND_HOURS))
            {
                broadcastRaw.SEND_HOURS = "00:00 - 01:00";
            }

            broadcastRaw.CHANNEL = broadcastNodeInfoByRegEx.GetChannel();
            if (string.IsNullOrEmpty(broadcastRaw.CHANNEL))
            {
                broadcastRaw.CHANNEL = "DR";
            }

            broadcastRaw.EPISODE = broadcastNodeInfoByRegEx.GetEpisode();
            broadcastRaw.GENRE = broadcastNodeInfoByRegEx.GetGenre();

            return broadcastRaw;
        }

        #region Helpers.

        /// <summary>
        /// Get media type from url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static EMediaType MediaTypeByUrl(string? url) =>
            string.IsNullOrEmpty(url) ?
            EMediaType.nomedia :
            url.Contains("radio") ? EMediaType.radio :
            url.Contains("tv") ? EMediaType.tv :
            EMediaType.nomedia;

        #endregion

    }
}

