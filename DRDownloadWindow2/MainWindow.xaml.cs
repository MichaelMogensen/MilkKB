using DRDownloadWindow2.DRBroadcast;
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
            UpdateWindow();
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

        private void UpdateStatusbar()
        {
        }

        /// <summary>
        /// Try to fill in broadcast if browser is no broadcast page.
        /// </summary>
        private void UpdateWindow()
        {
            // Try to read broadcast data from current url (it's ok to fail).
            var broadcast = new DRBroadcastHtmlScraper(Browser.GetPageHtml(), DRBroadcastHtmlScraper.MediaType(Browser.Url)).Broadcast;

            // Set main fields.
            tbTitle.Text = broadcast.Title;

            // Set technical fields.
            tbUniqueId.Text = $"{nameof(broadcast.UniqueId)}: {broadcast.UniqueId}";
            tbEntryId.Text = $"{nameof(broadcast.EntryId)}: {broadcast.EntryId}";

            tbUrl.Text = $"{nameof(Browser.Url)}: {Browser.Url}";
        }

        #endregion
    }
}