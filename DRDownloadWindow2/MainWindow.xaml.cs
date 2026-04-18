using DRDownloadWindow2.ViewModels;
using System.Windows;

namespace DRDownloadWindow2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            ViewModel.Browser.Close();
        }

        #endregion

        #region Methods.

        #endregion

    }

}

