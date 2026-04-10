using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System.Security.Policy;
using System.Windows;
using System.Windows.Input;

namespace DRDownloadWindow
{
    public partial class MainWindow : Window
    {
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
        /// Browse.
        /// </summary>
        private void GoToThatPage()
        {
            var url = urlTextBox.Text;
            rawHtmlTextBox.Text = ScrapePage(url);
        }

        /// <summary>
        /// Scrape page.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<string> ScrapePageAsync(string url)
        {
            ChromeOptions options = new()
            {
                BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                
            };

            //options.AddArgument("headless"); // :-) Now all html comes in! Minor: Can I hide/minimize browser?
            var chrome = new ChromeDriver(options);

            await chrome.Navigate().GoToUrlAsync(url);

            var html = chrome.PageSource;

            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document.ParsedText;
        }
        private static string ScrapePage(string url)
        {
            var task = Task.Run(async () => await ScrapePageAsync(url));
            task.Wait();
            var html = task.Result;

            return html;
        }


        #region Event handlers.

        private void OnUrlEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            { GoToThatPage(); }
        }

        private void OnGoToPageCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnGoToPageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            GoToThatPage();
        }

        #endregion


        public void Test()
        {
            //DRMedia.PipeOutput?.PipeMessageTo("Hej");

        }

    }
}

