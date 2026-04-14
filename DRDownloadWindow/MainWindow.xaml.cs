using DRDownload.Common.Types.BroadcastHtmlScraper;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Windows;
using System.Windows.Input;

namespace DRDownloadWindow
{
    public partial class MainWindow : Window
    {
        private static readonly string MainSearchPage = "https://www.kb.dk/find-materiale/dr-arkivet/";
        public static readonly string StartUrl = MainSearchPage;

        private ChromeDriver ChromeBrowser { get; set; } = new ChromeDriver(new ChromeOptions()
        {
            BinaryLocation = @"C:\Program Files\Google\Chrome\Application\Chrome.exe",
        });

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
            var html = LoadHtml(url);

            rawHtmlTextBox.Text = html;

            var broadcastHtmlScraper = new BroadcastHtmlScraper(url, html);
            if (broadcastHtmlScraper.BroadcastRecord == null)
            { return; }

            var broadcastRecordText = broadcastHtmlScraper.BroadcastRecord.ToString();
            broadcastRecordText = broadcastRecordText.Replace("|", Environment.NewLine);
            broadcastRecordTextBox.Text = broadcastRecordText;
        }

        /// <summary>
        /// Read all html of page.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string LoadHtml(string url)
        {
            ChromeBrowser.Navigate().GoToUrl(url);

            var wait = new WebDriverWait(ChromeBrowser, TimeSpan.FromSeconds(20));
            var element = wait.Until(d =>
            {
                try
                {
                    return d.FindElement(By.XPath("//div[@class=\"boardcast-record-data\"]"));
                }
                catch
                {
                    return null;
                }
            });

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

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ChromeBrowser.Quit();
        }

        #endregion

    }
}

//if (string.IsNullOrEmpty(broadcastHtmlScraper.BroadcastRecord.EntryId))
//{
//    MessageBox.Show("No EntryId found!", "Parse error");
//}
