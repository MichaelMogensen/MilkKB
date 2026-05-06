using DRDownloadWindow2.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace DRDownloadWindow2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// ViewModel holds everything.
        /// </summary>
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
        /// On window left mouse button down we drag window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// On window closing we close browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewModel.Model.Browser?.Close();
        }

        /// <summary>
        /// Hide buttons when tech. details are expanded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTechnicalDetailsExpanded(object sender, RoutedEventArgs e)
        {
            HideButtonGrid();
        }

        /// <summary>
        /// Show buttons when tech. details are collapsed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTechnicalDetailsCollapsed(object sender, RoutedEventArgs e)
        {
            ShowButtonGrid();
        }

        #endregion

        private void HideButtonGrid()
        {
            gridButtons.Visibility = Visibility.Hidden;
        }

        private void ShowButtonGrid()
        {
            gridButtons.Visibility = Visibility.Visible;
        }

    }

}

