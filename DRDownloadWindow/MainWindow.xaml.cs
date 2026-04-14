using DRDownload.Common.Types.BroadcastHtmlScraper;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System.Windows;
using System.Windows.Input;

namespace DRDownloadWindow
{
    public partial class MainWindow : Window
    {
        private ChromeDriver ChromeBrowser { get; set; } = new ChromeDriver(new ChromeOptions()
        {
            BinaryLocation = @"C:\Program Files\Google\Chrome\Application\Chrome.exe",
        });

        // Main search page: https://www.kb.dk/find-materiale/dr-arkivet/
        //
        public static readonly string StartUrl = "https://www.kb.dk/find-materiale/dr-arkivet/post/ds.tv:oai:io:666816aa-3fd7-422c-9786-f05f124f5b5a";

        /// <summary>
        /// Ctor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        /// <summary>
        /// Init.
        /// </summary>
        private void InitializeWindow()
        {
            urlTextBox.Text = StartUrl;
        }

        /// <summary>
        /// Navigate to url.
        /// </summary>
        private void Navigate()
        {
            var url = urlTextBox.Text;
            var html = ReadHtml(url);

            rawHtmlTextBox.Text = html;

            var broadcastHtmlScraper = new BroadcastHtmlScraper(url, html);
            var broadcastRecord = broadcastHtmlScraper.BroadcastRecord?.ToString();
            if (string.IsNullOrEmpty(broadcastRecord))
            {
                return;
            }

            broadcastRecord = broadcastRecord.Replace("|", Environment.NewLine);
            broadcastRecordTextBox.Text = broadcastRecord;
        }

        /// <summary>
        /// Read all html of page.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string ReadHtml(string url)
        {
            ChromeBrowser.Navigate().GoToUrl(url);

            var document = new HtmlDocument();
            document.LoadHtml(ChromeBrowser.PageSource);

            return document.ParsedText;
        }

        #region Event handlers.

        private void OnUrlEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            { Navigate(); }
        }

        private void OnGoToPageCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnGoToPageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Navigate();
        }

        #endregion

    }
}

