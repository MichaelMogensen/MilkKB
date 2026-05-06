using DRDownloadWindow2.DRBroadcast;
using DRDownloadWindow2.Extensions;
using DRDownloadWindow2.Models;
using DRDownloadWindow2.OneValueSettingFile;
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
                OnPropertyChanged(nameof(Title));
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
                OnPropertyChanged(nameof(SendDateAndDuration));
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
                OnPropertyChanged(nameof(Description));
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
                OnPropertyChanged(nameof(Episode));
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
                OnPropertyChanged(nameof(Channel));
            }
        }

        #endregion

        #region Genre.

        private string? _genre;
        public string? Genre
        {
            get { return _genre; }
            set
            {
                _genre = value.OrDefault("Ingen genre oplysninger");
                OnPropertyChanged(nameof(Genre));
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
                OnPropertyChanged(nameof(UniqueId));
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
                OnPropertyChanged(nameof(EntryId));
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
                OnPropertyChanged(nameof(MediaType));
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
                OnPropertyChanged(nameof(Url));
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
                OnPropertyChanged(nameof(DownloadFolder));
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
                OnPropertyChanged(nameof(Mp3File));
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
                OnPropertyChanged(nameof(M3uFile));
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
                OnPropertyChanged(nameof(Mp4File));
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
                OnPropertyChanged(nameof(LogFile));
            }
        }

        #endregion

        #region GenerateLogFile.

        private bool _generateLogFile;
        public bool GenerateLogFile
        {
            get { return _generateLogFile; }
            set
            {
                _generateLogFile = value;
                OnPropertyChanged(nameof(GenerateLogFile));
            }
        }

        #endregion

        #region DeleteM3uFileAfterDownload.

        private bool _deleteM3uFileAfterDownload;
        public bool DeleteM3uFileAfterDownload
        {
            get { return _deleteM3uFileAfterDownload; }
            set
            {
                _deleteM3uFileAfterDownload = value;
                OnPropertyChanged(nameof(DeleteM3uFileAfterDownload));
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
                                if (Model.Browser != null)
                                {
                                    var handler = new DRBroadcastHandler(Model.Browser);
                                    handler.ReadBroadcastDetails();
                                    Model.Broadcast = handler.Broadcast;
                                    Update();
                                }
                            });
                        },
                        s => true));
            }
        }

        #endregion

        #region Download command's.

        private ICommand? _downloadCommand0;
        public ICommand DownloadCommand0 => _downloadCommand0 ?? (_downloadCommand0 = new DelegateCommand(_ => Task.Run(async () => await StartDownloadAsync(value => Progressbar0 = value)), _ => true));        

        private ICommand? _downloadCommand1;
        public ICommand DownloadCommand1 => _downloadCommand1 ?? (_downloadCommand1 = new DelegateCommand(_ => Task.Run(async () => await StartDownloadAsync(value => Progressbar1 = value)), _ => true));        

        private ICommand? _downloadCommand2;
        public ICommand DownloadCommand2 => _downloadCommand2 ?? (_downloadCommand2 = new DelegateCommand(_ => Task.Run(async () => await StartDownloadAsync(value => Progressbar2 = value)), _ => true));        

        private ICommand? _downloadCommand3;
        public ICommand DownloadCommand3 => _downloadCommand3 ?? (_downloadCommand3 = new DelegateCommand(_ => Task.Run(async () => await StartDownloadAsync(value => Progressbar3 = value)), _ => true));        

        private ICommand? _downloadCommand4;
        public ICommand DownloadCommand4 => _downloadCommand4 ?? (_downloadCommand4 = new DelegateCommand(_ => Task.Run(async () => await StartDownloadAsync(value => Progressbar4 = value)), _ => true));        

        private ICommand? _downloadCommand5;
        public ICommand DownloadCommand5 => _downloadCommand5 ?? (_downloadCommand5 = new DelegateCommand(_ => Task.Run(async () => await StartDownloadAsync(value => Progressbar5 = value)), _ => true));        

        private ICommand? _downloadCommand6;
        public ICommand DownloadCommand6 => _downloadCommand6 ?? (_downloadCommand6 = new DelegateCommand(_ => Task.Run(async () => await StartDownloadAsync(value => Progressbar6 = value)), _ => true));        

        private ICommand? _downloadCommand7;
        public ICommand DownloadCommand7 => _downloadCommand7 ?? (_downloadCommand7 = new DelegateCommand(_ => Task.Run(async () => await StartDownloadAsync(value => Progressbar7 = value)), _ => true));        

        private ICommand? _downloadCommand8;
        public ICommand DownloadCommand8 => _downloadCommand8 ?? (_downloadCommand8 = new DelegateCommand(_ => Task.Run(async () => await StartDownloadAsync(value => Progressbar8 = value)), _ => true));        

        private ICommand? _downloadCommand9;
        public ICommand DownloadCommand9 => _downloadCommand9 ?? (_downloadCommand9 = new DelegateCommand(_ => Task.Run(async () => await StartDownloadAsync(value => Progressbar9 = value)), _ => true));        

        #endregion

        #region GenerateLogFile command.

        private ICommand? _generateLogFileCommand;
        public ICommand GenerateLogFileCommand
        {
            get
            {
                return _generateLogFileCommand ?? (_generateLogFileCommand =
                    new DelegateCommand(
                        s =>
                        {
                            new SaveOneValueInASettingFile(nameof(GenerateLogFile)).Value(GenerateLogFile);
                        },
                        s => true));
            }
        }

        #endregion

        #region DeleteM3uFileAfterDownload command.

        private ICommand? _deleteM3uFileAfterDownloadCommand;
        public ICommand DeleteM3uFileAfterDownloadCommand
        {
            get
            {
                return _deleteM3uFileAfterDownloadCommand ?? (_deleteM3uFileAfterDownloadCommand =
                    new DelegateCommand(
                        s =>
                        {
                            new SaveOneValueInASettingFile(nameof(DeleteM3uFileAfterDownload)).Value(DeleteM3uFileAfterDownload);
                        },
                        s => true));
            }
        }

        #endregion

        // Statusbar and progressbar.

        #region Statusbar.

        private string? _statusBar { get; set; }
        public string? StatusBar
        {
            get => _statusBar;
            set
            {
                _statusBar = value;
                OnPropertyChanged(nameof(StatusBar));
            }
        }

        private string? _statusBarColor { get; set; }
        public string? StatusBarColor
        {
            get => _statusBarColor;
            set
            {
                _statusBarColor = value;
                OnPropertyChanged(nameof(StatusBarColor));
            }
        }

        #endregion

        #region Progressbar's.

        private int? _progressbar0 { get; set; }
        public int? Progressbar0
        {
            get => _progressbar0;
            set
            {
                _progressbar0 = value;
                OnPropertyChanged(nameof(Progressbar0));
            }
        }

        private int? _progressbar1 { get; set; }
        public int? Progressbar1
        {
            get => _progressbar1;
            set
            {
                _progressbar1 = value;
                OnPropertyChanged(nameof(Progressbar1));
            }
        }
        private int? _progressbar2 { get; set; }
        public int? Progressbar2
        {
            get => _progressbar2;
            set
            {
                _progressbar2 = value;
                OnPropertyChanged(nameof(Progressbar2));
            }
        }

        private int? _progressbar3 { get; set; }
        public int? Progressbar3
        {
            get => _progressbar3;
            set
            {
                _progressbar3 = value;
                OnPropertyChanged(nameof(Progressbar3));
            }
        }

        private int? _progressbar4 { get; set; }
        public int? Progressbar4
        {
            get => _progressbar4;
            set
            {
                _progressbar4 = value;
                OnPropertyChanged(nameof(Progressbar4));
            }
        }

        private int? _progressbar5 { get; set; }
        public int? Progressbar5
        {
            get => _progressbar5;
            set
            {
                _progressbar5 = value;
                OnPropertyChanged(nameof(Progressbar5));
            }
        }

        private int? _progressbar6 { get; set; }
        public int? Progressbar6
        {
            get => _progressbar6;
            set
            {
                _progressbar6 = value;
                OnPropertyChanged(nameof(Progressbar6));
            }
        }

        private int? _progressbar7 { get; set; }
        public int? Progressbar7
        {
            get => _progressbar7;
            set
            {
                _progressbar7 = value;
                OnPropertyChanged(nameof(Progressbar7));
            }
        }

        private int? _progressbar8 { get; set; }
        public int? Progressbar8
        {
            get => _progressbar8;
            set
            {
                _progressbar8 = value;
                OnPropertyChanged(nameof(Progressbar8));
            }
        }

        private int? _progressbar9 { get; set; }
        public int? Progressbar9
        {
            get => _progressbar9;
            set
            {
                _progressbar9 = value;
                OnPropertyChanged(nameof(Progressbar9));
            }
        }

        #endregion

        // Other.

        #region Methods.

        /// <summary>
        /// Start download 0, 1, ..., N.
        /// </summary>
        /// <param name="SetPG"></param>
        /// <returns></returns>
        private async Task StartDownloadAsync(Action<int> SetPG)
        {
            await new DRMedia(
                Model.Broadcast,
                new StatusAndProgressHandler(
                    new UIDispatchUpdater<UIElementProps<string>>(e =>
                    {
                        StatusBar = e.Value;
                        StatusBarColor = Util.WarningLevelToColor(e.WarningLevel);
                    }),
                    new UIDispatchUpdater<UIElementProps<int>>(e =>
                    {
                        SetPG(e.Value);
                    }))).
                StartDownloadAsync(new CancellationToken());
        }

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

            GenerateLogFile = Model.Broadcast.GenerateLogFile;
            DeleteM3uFileAfterDownload = Model.Broadcast.DeleteM3uFileAfterDownload;

            StatusBar = "Klar";
            StatusBarColor = Util.WarningLevelToColor(EWarningLevel.info);

            Progressbar0 = 0;
            Progressbar1 = 0;
            Progressbar2 = 0;
            Progressbar3 = 0;
            Progressbar4 = 0;
            Progressbar5 = 0;
            Progressbar6 = 0;
            Progressbar7 = 0;
            Progressbar8 = 0;
            Progressbar9 = 0;
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

