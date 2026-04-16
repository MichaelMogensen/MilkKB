using DRDownloadWindow2.DRBroadcast;
using DRDownloadWindow2.Types;
using DRDownloadWindow2.Utilities;
using System.Windows;
using System.Windows.Input;

namespace DRDownloadWindow2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string MainSearchPage = "https://www.kb.dk/find-materiale/dr-arkivet/";

        public ChromeBrowser Browser { get; set; } = new ChromeBrowser();

        public MainWindow()
        {
            InitializeComponent();
            InitializeUsecase();
        }

        #region Message handlers.

        /// <summary>
        /// On copy url.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCopyUrl(object sender, MouseButtonEventArgs e)
        {
            CmdCopyUrl();
        }

        /// <summary>
        /// On download.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDownload(object sender, MouseButtonEventArgs e)
        {
            CmdDownload();
        }

        /// <summary>
        /// On close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClose(object sender, MouseButtonEventArgs e)
        {
            CmdClose();
        }

        /// <summary>
        /// On window closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Browser.Close();
        }
        
        #endregion

        #region Methods.

        private void CmdCopyUrl()
        {
            UpdateWindowWith(PotentialBroadcast());
        }

        private void CmdDownload()
        {
            MessageBox.Show("Download");
        }

        private void CmdClose()
        {
            Close();
        }

        private void InitializeUsecase()
        {
            Browser.GoToUrl(MainSearchPage);
        }

        /// <summary>
        /// If page has broadcast details full broadcast object is constructed here. If not object has only its Url 
        /// set and rest of properties is null.
        /// </summary>
        /// <returns></returns>
        private Broadcast PotentialBroadcast()
        {
            var url = Browser.Url;
            var html = DRBroadcastHtmlScraper.MediaTypeByUrl(url) == EMediaType.nomedia ? 
                Browser.GetPageHtml() : // Holds crab not relevant for us.
                Browser.GetPageHtml(DRBroadcastHtmlScraper.BROADCAST_REC_DATA_PARENT_XPATH); // Holds broadcast details.

            var broadcast = new DRBroadcastHtmlScraper(url, html).Broadcast;

            return broadcast;
        }

        /// <summary>
        /// Show message.
        /// </summary>
        /// <param name="msg"></param>
        private void UpdateStatusbarWith(string msg)
        {
        }

        /// <summary>
        /// Fill in potential broadcast.
        /// </summary>
        /// <param name="broadcast"></param>
        private void UpdateWindowWith(Broadcast broadcast)
        {
            // Set main fields.
            tbTitle.Text = Util.OrDefault(broadcast.Title, 
                "Udsendelse");
            tbDate.Text = Util.OrDefault(Util.ToDanishDateAndDuration(broadcast.SendDate, broadcast.Duration), 
                "Dato");
            tbDescription.Text = Util.OrDefault(broadcast.Description, 
                "Beskrivelse");
            tbEpisode.Text = Util.OrDefault(broadcast.Episode, 
                "Episode");
            tbChannel.Text = Util.OrDefault(broadcast.Channel, 
                "Kanal");
            tbGenre.Text = Util.OrDefault(broadcast.Genre, 
                "Genre");

            // Set technical fields.
            tbUniqueId.Text = $"{nameof(broadcast.UniqueId)} = {Util.OrNil(broadcast.UniqueId)}";
            tbEntryId.Text = $"{nameof(broadcast.EntryId)} = {Util.OrNil(broadcast.EntryId)}";
            tbMediaType.Text = $"{nameof(broadcast.MediaType)} = {Util.OrNil(broadcast.MediaType)}";
            tbUrl.Text = $"{nameof(Browser.Url)} = {Util.OrNil(Browser.Url)}";
            tbDownloadFolder.Text = $"{nameof(broadcast.DownloadFolder)} = {Util.OrNil(broadcast.DownloadFolder)}";
            tbmp3File.Text = $"{nameof(broadcast.Mp3File)} = {Util.OrNil(broadcast.Mp3File)}";
            tbm3uFile.Text = $"{nameof(broadcast.M3uFile)} = {Util.OrNil(broadcast.M3uFile)}";
            tbmp4File.Text = $"{nameof(broadcast.Mp4File)} = {Util.OrNil(broadcast.Mp4File)}";
            tblogFile.Text = $"{nameof(broadcast.LogFile)} = {Util.OrNil(broadcast.LogFile)}";
        }

        #endregion
    }
}