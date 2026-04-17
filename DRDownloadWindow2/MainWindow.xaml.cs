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

        private ChromeBrowser Browser { get; set; } = new ChromeBrowser();

        private Broadcast Broadcast { get; set; } = new Broadcast();

        /// <summary>
        /// Ctor.
        /// </summary>
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
            ReadPotentialBroadcastByPageHtml();
            UpdateWindow();
        }

        private void CmdDownload()
        {
            sbMessage.Text = "50%";
            pbDownload.Value = 50;

        }

        private void CmdClose()
        {
            Close();
        }

        private void InitializeUsecase()
        {
            pbDownload.Minimum = 0;
            pbDownload.Maximum = 100;

            Browser.GoToUrl(MainSearchPage);
        }

        /// <summary>
        /// If page has broadcast details full broadcast object is constructed here. If not, object has only its Url 
        /// set and rest of properties is null.
        /// </summary>
        /// <returns></returns>
        private void ReadPotentialBroadcastByPageHtml()
        {
            var url = Browser.Url;
            var html = DRBroadcastHtmlScraper.MediaTypeByUrl(url) == EMediaType.nomedia ? 
                Browser.GetPageHtml() : // Holds crab not relevant for us.
                Browser.GetPageHtml(DRBroadcastHtmlScraper.BROADCAST_REC_DATA_PARENT_XPATH); // Holds broadcast details.

            Broadcast = new DRBroadcastHtmlScraper(url, html).Broadcast;
        }

        /// <summary>
        /// Update message.
        /// </summary>
        /// <param name="msg"></param>
        private void UpdateStatusbar(string msg)
        {
        }

        /// <summary>
        /// Update window.
        /// </summary>
        private void UpdateWindow()
        {
            // Set main fields.
            tbTitle.Text = Util.OrDefault(Broadcast.Title, 
                "Udsendelse");
            tbDate.Text = Util.OrDefault(Util.ToDanishDateAndDuration(Broadcast.SendDate, Broadcast.Duration), 
                "Dato");
            tbDescription.Text = Util.OrDefault(Broadcast.Description, 
                "Beskrivelse");
            tbEpisode.Text = Util.OrDefault(Broadcast.Episode, 
                "Episode");
            tbChannel.Text = Util.OrDefault(Broadcast.Channel, 
                "Kanal");
            tbGenre.Text = Util.OrDefault(Broadcast.Genre, 
                "Genre");

            // Set technical fields.
            tbUniqueId.Text = $"{nameof(Broadcast.UniqueId)} = {Util.OrNil(Broadcast.UniqueId)}";
            tbEntryId.Text = $"{nameof(Broadcast.EntryId)} = {Util.OrNil(Broadcast.EntryId)}";
            tbMediaType.Text = $"{nameof(Broadcast.MediaType)} = {Util.OrNil(Broadcast.MediaType)}";
            tbUrl.Text = $"{nameof(Browser.Url)} = {Util.OrNil(Browser.Url)}";
            tbDownloadFolder.Text = $"{nameof(Broadcast.DownloadFolder)} = {Util.OrNil(Broadcast.DownloadFolder)}";
            tbmp3File.Text = $"{nameof(Broadcast.Mp3File)} = {Util.OrNil(Broadcast.Mp3File)}";
            tbm3uFile.Text = $"{nameof(Broadcast.M3uFile)} = {Util.OrNil(Broadcast.M3uFile)}";
            tbmp4File.Text = $"{nameof(Broadcast.Mp4File)} = {Util.OrNil(Broadcast.Mp4File)}";
            tblogFile.Text = $"{nameof(Broadcast.LogFile)} = {Util.OrNil(Broadcast.LogFile)}";
        }

        #endregion
    }
}

