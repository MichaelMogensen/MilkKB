using DRDownload.Media;
using System.Windows;

namespace DRDownloadWindow
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

        public void Test()
        {
            DRMedia.PipeOutput?.PipeMessageTo("Hej");

        }

    }
}