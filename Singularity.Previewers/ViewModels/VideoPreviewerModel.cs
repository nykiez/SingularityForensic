using EventLogger;
using Prism.Commands;
using Prism.Mvvm;
using Singularity.Previewers.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;

namespace Singularity.Previewers.ViewModels {
    public class VlcVideoPreviewerModel: VideoPreviewerModel<VlcPlayer> {
        public VlcVideoPreviewerModel(VlcPlayer player):base(player) {

        }
        
    }

    public interface IVideoPreviewerModel<out TPlayer> : IDisposable where TPlayer : IPlayer {
        TPlayer Player { get; }
    }

    public abstract partial class VideoPreviewerModel<TPlayer> : BindableBase,
        IVideoPreviewerModel<TPlayer> where TPlayer : IPlayer {

        public VideoPreviewerModel(TPlayer player) {
            this.Player = player;
        }
        
        public TPlayer Player { get; private set; }

        //private DelegateCommand _addSongCommand;
        //public DelegateCommand AddSongCommand => _addSongCommand ??
        //    (_addSongCommand = new DelegateCommand(
        //        () => {
        //            var dialog = new VistaOpenFileDialog();
        //            dialog.Multiselect = false;
        //            if (dialog.ShowDialog() == true) {
        //                try {
        //                    var player = new DHPlayer(dialog.FileName);
        //                    Play(player);

        //                    var item = new SongItem(player) {
        //                        SongName = dialog.FileName.Substring(dialog.FileName.LastIndexOf("\\") + 1),
        //                    };

        //                    item.PreSource = Player.GetImageSource(TimeSpan.FromSeconds(2));
        //                    item.SongLength = Player.TotalTimeSpan;
        //                    SongItems.Add(item);
        //                    SelectedSong = item;
        //                    IsPlaying = true;
        //                }
        //                catch {
        //                    CDFCMessageBox.Show("Error when constructing player!");
        //                }
        //            }
        //        }
        //    ));
        
        /// <summary>
        /// 重新开始一次播放;
        /// </summary>
        /// <param name="player"></param>
        private void Play(TPlayer player) {
            Player?.Dispose();
            Player = player;
            player.Play();
            player.SetVolume((uint)Volume);
            var totalTs = player.TotalTimeSpan;
            if (totalTs != null) {
                MaxTimeSpan = totalTs.Value;
                PlayValue = 0;
            }
            UpdateProcess();
        }

        private long priLevel = 0;

        /// <summary>
        /// 部署进度升级;
        /// </summary>
        private void UpdateProcess() {
            //记录当前优先等级;
            var curLevel = ++priLevel;
            //部署升级进度条升级;
            ThreadPool.QueueUserWorkItem(cb => {
                disposeEvt.Reset();
                while (curLevel == priLevel && Player != null) {
                    var curTs = Player.CurrentTimeSpan;
                    if (curTs != null && !IsProcessing) {
                        if (_disposed) {
                            disposeEvt?.Set();
                            return;
                        }

                        try {
                            Application.Current?.Dispatcher.Invoke(() => {
                                _playValue = curTs.Value.TotalSeconds;
                                CurrentTimeSpan = TimeSpan.FromSeconds((long)curTs.Value.TotalSeconds);
                                MaxTimeSpan = Player.TotalTimeSpan ?? TimeSpan.Zero;
                                RaisePropertyChanged(nameof(PlayValue));
                            });
                        }
                        catch(Exception ex) {
                            Logger.WriteCallerLine(ex.Message);
                        }
                    }
                    Thread.Sleep(1000);
                }
                if (_disposed) {
                    disposeEvt?.Set();
                    return;
                }
            });
        }

        private bool _disposed;

        /// <summary>
        /// 释放事件锁;这避免了读取进度的行为可能会发生在释放接口后,导致异常发生;
        /// </summary>
        private ManualResetEvent disposeEvt = new ManualResetEvent(true);

        /// <summary>
        /// 释放播放器;
        /// </summary>
        public virtual void Dispose() {
            try {
                priLevel++;
                _disposed = true;
                //等待占用Player的动作发出信号,便可以进行释放;
                disposeEvt.WaitOne();
                Player?.Dispose();
            }
            catch(Exception ex) {
                Logger.WriteLine(ex.Message);
                throw;
            }
        }

        private bool _isProcessing;
        public bool IsProcessing {
            get {
                return _isProcessing;
            }
            set {
                SetProperty(ref _isProcessing, value);
            }
        }

        private TimeSpan _maxTimeSpan;
        public TimeSpan MaxTimeSpan {
            get {
                return _maxTimeSpan;
            }
            set {
                SetProperty(ref _maxTimeSpan, value);
                RaisePropertyChanged(nameof(MaxTimeLength));
            }
        }
        public double MaxTimeLength => MaxTimeSpan.TotalSeconds;

        private TimeSpan _currentTimeSpan;
        public TimeSpan CurrentTimeSpan {
            get {
                return _currentTimeSpan;
            }
            set {
                SetProperty(ref _currentTimeSpan, value);
            }
        }

        private double _playValue;
        public double PlayValue {
            get {
                return _playValue;
            }
            set {
                SetProperty(ref _playValue, value);
                if (Player != null) {
                    Player.EscapeToTimeSpan(TimeSpan.FromSeconds(value));
                }
            }
        }

        private SongItem _selectedSong;
        public SongItem SelectedSong {
            get {
                return _selectedSong;
            }
            set {
                SetProperty(ref _selectedSong, value);
            }
        }

        public ObservableCollection<SongItem> SongItems { get; set; } = new ObservableCollection<SongItem>();

        private bool _isPlaying;
        public bool IsPlaying {
            get {
                return _isPlaying;
            }
            set {
                SetProperty(ref _isPlaying, value);
            }
        }

        private DelegateCommand _playOrPauseCommand;
        public DelegateCommand PlayOrPauseCommand => _playOrPauseCommand ??
            (_playOrPauseCommand = new DelegateCommand(
                () => {
                    if (IsPlaying) {
                        try {
                            if (Player?.Pause() == true) {
                                IsPlaying = false;
                            }
                        }
                        catch {

                        }
                    }
                    else {
                        try {
                            if (Player?.Resume() == true) {
                                IsPlaying = true;
                            }
                            UpdateProcess();
                        }
                        catch (Exception ex) {
                            Logger.WriteLine($"{nameof(VideoPreviewerModel<TPlayer>)}->{nameof(PlayOrPauseCommand)}:{ex.Message}");
                            MessageBox.Show("Failed to Resume");
                        }
                    }
                }
            ));

        //private DelegateCommand _removeSongCommand;
        //public DelegateCommand RemoveSongCommand => _removeSongCommand ??
        //    (_removeSongCommand = new DelegateCommand(
        //        () => {
        //            var removeItems = new List<SongItem>();
        //            foreach (var item in SongItems.Where(p => p.IsChecked)) {
        //                try {
        //                    //SongItems.Remove()
        //                    removeItems.Add(item);
        //                    item.Player.Dispose();
        //                    if (Player == item.Player) {
        //                        Player = null;
        //                        priLevel++;
        //                        MaxTimeSpan = TimeSpan.Zero;
        //                        CurrentTimeSpan = TimeSpan.Zero;
        //                    }
        //                }
        //                catch (Exception ex) {
        //                    Logger.WriteLine($"{nameof(VideoPreviewerModel<TPlayer>)}->{nameof(RemoveSongCommand)}:{ex.Message}");
        //                    CDFCMessageBox.Show(ex.Message);
        //                }
        //            }
        //            removeItems.ForEach(p => SongItems.Remove(p));
        //        }
        //    ));

        private DelegateCommand _closingCommand;
        public DelegateCommand ClosingCommand => _closingCommand ??
            (_closingCommand = new DelegateCommand(
                () => {
                    foreach (var item in SongItems) {
                        try {
                            item.Player.Dispose();
                        }
                        catch (Exception ex) {
                            Logger.WriteLine($"{nameof(VideoPreviewerModel<TPlayer>)}->{nameof(ClosingCommand)}:{ex.Message}");
                        }
                    }
                    priLevel++;
                }
            ));

        private DelegateCommand<double?> _goForwardCommand;
        public DelegateCommand<double?> GoForwardCommand =>
            _goForwardCommand = (_goForwardCommand = new DelegateCommand<double?>(
                dis => {
                    if(dis == null) {
                        return;
                    }
                    if (Player == null) {
                        return;
                    }

                    var curTs = Player.CurrentTimeSpan;
                    if (curTs != null) {
                        //if (curTs.Value.Add(TimeSpan.FromSeconds(dis)) <= Player.TotalTimeSpan) {
                        var s = Player.EscapeToTimeSpan(curTs.Value.Add(TimeSpan.FromSeconds(dis.Value)));
                        //}
                    }

                }));

        //private DelegateCommand _choseSongCommand;
        //public DelegateCommand ChoseSongCommand => _choseSongCommand ??
        //    (_choseSongCommand = new DelegateCommand(
        //        () => {
        //            if (SelectedSong != null) {
        //                Play(SelectedSong.Player);
        //            }
        //        }
        //    ));

        private DelegateCommand _stopCommand;
        public DelegateCommand StopCommand => _stopCommand ??
            (_stopCommand = new DelegateCommand(
                () => {
                    Player.Stop();
                    IsPlaying = false;
                }
            ));
    }
    
    //音频部分;
    public abstract partial class VideoPreviewerModel<TPlayer> {
        private double _volume = 100;
        public double Volume {
            get {
                return _volume;
            }
            set {
                SetProperty(ref _volume, value);
                Player?.SetVolume((uint)value);
            }
        }

        private double _maxVolume = 100;
        public double MaxVolume {
            get {
                return _maxVolume;
            }
            set {
                SetProperty(ref _maxVolume, value);
            }
        }
    }
}
