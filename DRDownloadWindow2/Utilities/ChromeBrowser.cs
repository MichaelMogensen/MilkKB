using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DRDownloadWindow2.Utilities
{
    public class ChromeBrowser
    {
        public string? Url { get => Browser.Url; set { GoToUrl(value); } }

        private ChromeDriver Browser { get; set; } = new ChromeDriver(new ChromeOptions()
        {
            BinaryLocation = @"C:\Program Files\Google\Chrome\Application\Chrome.exe",
        });

        /// <summary>
        /// Ctor.
        /// </summary>
        public ChromeBrowser()
        {

        }

        /// <summary>
        /// Quit browser.
        /// </summary>
        public void Close()
        {
            Browser.Quit();
        }

        /// <summary>
        /// Get html of page.
        /// </summary>
        /// <returns></returns>
        public string GetPageHtml()
        {
            var document = new HtmlDocument();
            document.LoadHtml(Browser.PageSource);

            return document.ParsedText;
        }

        /// <summary>
        /// Wait max 10s for something to complete and then get html of page.
        /// </summary>
        /// <param name="xpathToWaitFor"></param>
        /// <returns></returns>
        public string GetPageHtml(string xpathToWaitFor)
        {
            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(10));
            var element = wait.Until(d =>
            {
                try
                {
                    return d.FindElement(By.XPath(xpathToWaitFor));
                }
                catch
                {
                    return null;
                }
            });

            return GetPageHtml();
        }

        /// <summary>
        /// Navigate.
        /// </summary>
        /// <param name="url"></param>
        private void GoToUrl(string? url)
        {
            if (string.IsNullOrEmpty(url))
            { return; }

            Browser.Navigate().GoToUrl(url);
        }

    }
}

