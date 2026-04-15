using System.Windows;
using System.Windows.Input;

namespace DRDownloadWindow2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // TODO: BackgroundWorker to update SB.

        #region Message handlers.

        private void OnCopyUrl(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Copy url");
        }

        private void OnDownload(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Download");
        }

        private void OnClose(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        #endregion
    }
}