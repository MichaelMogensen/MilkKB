using DRDownloadWindow2.ViewModels;
using System.Windows;

namespace DRDownloadWindow2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string MainSearchPage = "https://www.kb.dk/find-materiale/dr-arkivet/";

        public IBroadcastViewModel ViewModel
        {
            get { return (IBroadcastViewModel)DataContext; }
            set { DataContext = value; }
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new BroadcastViewModel();

            ViewModel.Title = "Hejsa";

            //ViewModel.Browser.GoToUrl(MainSearchPage);
        }

        #region Message handlers.

        /// <summary>
        /// On close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBtnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// On window closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Browser.Close();
        }

        #endregion

        #region Methods.

        #endregion

    }

}

