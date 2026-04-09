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

            // TODO: Browse using ...

            MessageBox.Show(url);
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

