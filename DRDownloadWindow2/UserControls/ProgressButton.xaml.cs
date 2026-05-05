using System.Windows.Controls;
using System.Windows.Data;

namespace DRDownloadWindow2.UserControls
{
    /// <summary>
    /// Interaction logic for ProgressButton.xaml
    /// </summary>
    public partial class ProgressButton : UserControl
    {
        public Binding Value
        {
            set { BindingOperations.SetBinding(innerProgressbar, ProgressBar.ValueProperty, value); }
        }

        public Binding Command
        {
            set { BindingOperations.SetBinding(innerButton, Button.CommandProperty, value); }
        }

        public ProgressButton()
        {
            InitializeComponent();
        }

    }
}

