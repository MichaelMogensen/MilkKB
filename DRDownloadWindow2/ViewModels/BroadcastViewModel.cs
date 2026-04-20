using DRDownloadWindow2.DRBroadcast;
using DRDownloadWindow2.Extensions;
using DRDownloadWindow2.Models;
using DRDownloadWindow2.Types;
using DRDownloadWindow2.Utilities;
using System.ComponentModel;
using System.Windows.Input;

namespace DRDownloadWindow2.ViewModels
{
    public class BroadcastViewModel : INotifyPropertyChanged, IBroadcastViewModel
    {
        public BroadcastModel Model { get; set; } = new BroadcastModel();

        /// <summary>
        /// Ctor.
        /// </summary>
        public BroadcastViewModel()
        {
            Update();
        }

        // User prop's.

        #region Title.

        private string? _title;
        public string? Title
        {
            get { return _title; }
            set
            {
                _title = value.OrDefault("Udsendelse");
                OnPropertyChanged("Title");
            }
        }

        #endregion

        #region SendDateAndDuration.

        private string? _sendDateAndDuration;
        public string? SendDateAndDuration
        {
            get { return _sendDateAndDuration; }
            set
            {
                _sendDateAndDuration = value.OrDefault("Dato og varighed");
                OnPropertyChanged("SendDateAndDuration");
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
                _description = value.OrDefault("Ingen beskrivelse");
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
                _episode = value.OrDefault("Ingen episode oplysninger");
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
                _channel = value.OrDefault("Ingen kanal oplysninger");
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
                _genre = value.OrDefault("Ingen genre oplysninger");
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
                                // Here we expect user has already navigated to broadcast page.
                                var handler = new DRBroadcastHandler(Model.Browser);
                                handler.ScrapeBroadcastDetails();
                                Model.Broadcast = handler.Broadcast;
                                Update();

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
                                // TODO.
                            });
                        },
                        s => true));
            }
        }

        #endregion

        // Statusbar and progressbar.

        #region Statusbar.

        private string? _statusBarText { get; set; }
        public string? StatusBarText
        {
            get => _statusBarText;
            set
            {
                _statusBarText = value;
                OnPropertyChanged("StatusBarText");
            }
        }

        #endregion

        #region Progressbar.

        private int? _progressBarPercent { get; set; }
        public int? ProgressBarPercent
        {
            get => _progressBarPercent;
            set
            {
                _progressBarPercent = value;
                OnPropertyChanged("ProgressBarPercent");
            }
        }

        #endregion

        // Other.

        #region Methods.

        /// <summary>
        /// Sync. viewmodel and model.
        /// </summary>
        public void Update()
        {
            // User prop's.
            Title = Model.Broadcast.Title;
            SendDateAndDuration = Util.ToDanishDateAndDuration(Model.Broadcast.SendDate, Model.Broadcast.Duration);
            Description = Model.Broadcast.Description;
            Episode = Model.Broadcast.Episode;
            Channel = Model.Broadcast.Channel;
            Genre = Model.Broadcast.Genre;

            // Technical prop's.
            UniqueId = Model.Broadcast.UniqueId;
            EntryId = Model.Broadcast.EntryId;
            MediaType = Model.Broadcast.MediaType == EMediaType.nomedia ? null : Model.Broadcast.MediaType.ToString();
            Url = Model.Broadcast.Url;
            DownloadFolder = Model.Broadcast.DownloadFolder;
            Mp3File = Model.Broadcast.Mp3File;
            M3uFile = Model.Broadcast.M3uFile;
            Mp4File = Model.Broadcast.Mp4File;
            LogFile = Model.Broadcast.LogFile;
        }

        #endregion

        #region Background methods.

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

