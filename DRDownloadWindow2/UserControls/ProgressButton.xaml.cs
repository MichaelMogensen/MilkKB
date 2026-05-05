using System.Windows.Controls;
using System.Windows.Data;

namespace DRDownloadWindow2.UserControls
{
    /// <summary>
    /// Interaction logic for ProgressButton.xaml
    /// </summary>
    public partial class ProgressButton : UserControl
    {
        public Binding ValueBinding
        {
            set
            {
                BindingOperations.SetBinding(progressBar, ProgressBar.ValueProperty, value);
            }
        }

        public ProgressButton()
        {
            InitializeComponent();
        }
    }
}

/*
 
 
// Source - https://stackoverflow.com/q/7525185
// Posted by Willem, modified by community. See post 'Timeline' for change history
// Retrieved 2026-05-05, License - CC BY-SA 3.0

Binding myBinding = new Binding("SomeString");
myBinding.Source = ViewModel.SomeString;
myBinding.Mode = BindingMode.TwoWay;
myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
BindingOperations.SetBinding(txtText, TextBox.TextProperty, myBinding);

 */

