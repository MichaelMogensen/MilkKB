using DRDownloadWindow2.Extensions;
using DRDownloadWindow2.Types;
using System.ComponentModel;
using System.Windows.Input;

namespace DRDownloadWindow2.ViewModels
{
    public class BroadcastViewModel : INotifyPropertyChanged, IBroadcastViewModel
    {
        // TODO: Consider move to DataModel.
        private static readonly string MainSearchPage = "https://www.kb.dk/find-materiale/dr-arkivet/";

        /// <summary>
        /// Ctor.
        /// </summary>
        public BroadcastViewModel()
        {
            // User prop's.
            Title = "Udsendelse"; // TODO: OrDefault().
            Date = "Dato";
            Description = "Beskrivelse";
            Episode = "Episode";
            Channel = "Kanel";
            Genre = "Genre";

            // Technical prop's.
            UniqueId = null;
            EntryId = null;
            MediaType = null;
            Url = MainSearchPage;
            DownloadFolder = null;
            Mp3File = null;
            M3uFile = null;
            Mp4File = null;
            LogFile = null;
        }

        // TODO: Consider move to DataModel.
        #region Browser.

        public ChromeBrowser Browser { get; set; } = new ChromeBrowser();

        #endregion

        // User prop's.

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

        #region Date.

        private string? _date;
        public string? Date
        {
            get { return _date; }
            set
            {
                if (value == _date)
                    return;

                _date = value;
                OnPropertyChanged("Date");
            }
        }

        #endregion

        #region Description.

        private string? _description;
        public string? Description
        {
            get { return _description; }
            set
            {
                if (value == _description)
                    return;

                _description = value;
                OnPropertyChanged("Description");
            }
        }

        #endregion

        #region Episode.

        private string? _episode;
        public string? Episode
        {
            get { return _episode; }
            set
            {
                if (value == _episode)
                    return;

                _episode = value;
                OnPropertyChanged("Episode");
            }
        }

        #endregion

        #region Channel.

        private string? _channel;
        public string? Channel
        {
            get { return _channel; }
            set
            {
                if (value == _channel)
                    return;

                _channel = value;
                OnPropertyChanged("Channel");
            }
        }

        #endregion

        #region Genre

        private string? _genre;
        public string? Genre
        {
            get { return _genre; }
            set
            {
                if (value == _genre)
                    return;

                _genre = value;
                OnPropertyChanged("Genre");
            }
        }

        #endregion

        // Technical prop's.

        #region UniqueId.

        private string? _uniqueId;
        public string? UniqueId
        {
            get { return _uniqueId; }
            set
            {
                _uniqueId = $"{nameof(UniqueId)} = {value.OrNil()}";
                OnPropertyChanged("UniqueId");
            }
        }

        #endregion

        #region EntryId.

        private string? _entryId;
        public string? EntryId
        {
            get { return _entryId; }
            set
            {
                _entryId = $"{nameof(EntryId)} = {value.OrNil()}";
                OnPropertyChanged("EntryId");
            }
        }

        #endregion

        #region MediaType.

        private string? _mediaType;
        public string? MediaType
        {
            get { return _mediaType; }
            set
            {
                _mediaType = $"{nameof(MediaType)} = {value.OrNil()}";
                OnPropertyChanged("MediaType");
            }
        }

        #endregion

        #region Url.

        private string? _url;
        public string? Url
        {
            get { return _url; }
            set
            {
                _url = $"{nameof(Url)} = {value.OrNil()}";
                OnPropertyChanged("Url");
            }
        }

        #endregion

        #region DownloadFolder.

        private string? _downloadFolder;
        public string? DownloadFolder
        {
            get { return _downloadFolder; }
            set
            {
                _downloadFolder = $"{nameof(DownloadFolder)} = {value.OrNil()}";
                OnPropertyChanged("DownloadFolder");
            }
        }

        #endregion

        #region Mp3File.

        private string? _mp3File;
        public string? Mp3File
        {
            get { return _mp3File; }
            set
            {
                _mp3File = $"{nameof(Mp3File)} = {value.OrNil()}";
                OnPropertyChanged("Mp3File");
            }
        }

        #endregion

        #region M3uFile.

        private string? _m3uFile;
        public string? M3uFile
        {
            get { return _m3uFile; }
            set
            {
                _m3uFile = $"{nameof(M3uFile)} = {value.OrNil()}";
                OnPropertyChanged("M3uFile");
            }
        }

        #endregion

        #region Mp4File.

        private string? _mp4File;
        public string? Mp4File
        {
            get { return _mp4File; }
            set
            {
                _mp4File = $"{nameof(Mp4File)} = {value.OrNil()}";
                OnPropertyChanged("Mp4File");
            }
        }

        #endregion

        #region LogFile.

        private string? _logFile;
        public string? LogFile
        {
            get { return _logFile; }
            set
            {
                _logFile = $"{nameof(LogFile)} = {value.OrNil()}";
                OnPropertyChanged("LogFile");
            }
        }

        #endregion

        // Commands.

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
                                Browser.GoToUrl(Url);
                            });
                        },
                        s => true));
            }
        }

        #endregion

        #region Download command.

        private ICommand? _downloadCommand;
        public ICommand DownloadCommand
        {
            get
            {
                return _downloadCommand ?? (_downloadCommand =
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



        #region Property change.

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}


//public ChromeBrowser Browser { get; set; } = new ChromeBrowser();
//public Broadcast Broadcast { get; set; } = new Broadcast();
