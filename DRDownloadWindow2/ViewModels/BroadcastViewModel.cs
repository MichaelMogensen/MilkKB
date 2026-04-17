using DRDownloadWindow2.Types;
using System.ComponentModel;
using System.Windows.Input;

namespace DRDownloadWindow2.ViewModels
{
    public class BroadcastViewModel : INotifyPropertyChanged, IBroadcastViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Title.

        private string? _title;
        public string? Title
        {
            get { return _title; }
            set
            {
                if (value == _title)
                    return;

                _title = value;
                OnPropertyChanged("Title");
            }
        }

        #endregion




        #region Copy command.

        private ICommand? _copyCommand;
        public ICommand CopyCommand
        {
            get
            {
                return _copyCommand ?? (_copyCommand =
                    new DelegateCommand(
                        s =>
                        {
                            Task.Run(() =>
                            {

                            });
                        },
                        s => true));
            }
        }

        #endregion

        #region Start command.

        private ICommand? _startDownloadCommand;
        public ICommand StartDownloadCommand
        {
            get
            {
                return _startDownloadCommand ?? (_startDownloadCommand =
                    new DelegateCommand(
                        s =>
                        {
                            Task.Run(() =>
                            {

                            });
                        },
                        s => true));
            }
        }

        #endregion

    }
}


//public ChromeBrowser Browser { get; set; } = new ChromeBrowser();
//public Broadcast Broadcast { get; set; } = new Broadcast();
