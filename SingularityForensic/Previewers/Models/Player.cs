using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using System;
using System.Windows.Media;

namespace SingularityForensic.Previewers.Models {
    public interface IPlayer : IDisposable {
        bool Play();                        //播放接口;
        bool Pause();                       //暂停接口;
        bool Resume();                      //继续接口;
        bool Stop();                        //停止接口;
        bool EscapeToTimeSpan(TimeSpan ts); //跳转指令(ts目标进度);
        TimeSpan? CurrentTimeSpan { get; }      //当前进度;
        TimeSpan? TotalTimeSpan { get; }        //总长度;
        ImageSource GetImageSource(TimeSpan ts);    //获得某个时刻的图片流;
        string FileName { get; }
        bool SetVolume(uint val);                //设定音量大小;满值为100;
    }

    //VLC视频播放封装实体;
    public class VlcPlayer:IPlayer {
        public VlcPlayer(Meta.Vlc.Wpf.VlcPlayer _player) {
            Player = _player ?? throw new ArgumentNullException(nameof(_player));
        }

        public Meta.Vlc.Wpf.VlcPlayer Player { get; }

        public TimeSpan? CurrentTimeSpan => Player.Time;

        public TimeSpan? TotalTimeSpan => Player.Length;

        private string _fileName;
        public string FileName {
            get => _fileName;
            set {
                try {
                    Player.Stop();
                    Player.LoadMedia(value);
                    _fileName = value;
                }
                catch(Exception ex) {
                    LoggerService.Current?.WriteCallerLine(ex.Message);
                    throw;
                }
            }
        }

        public void Dispose() {
            try {
                Player.Dispose();
            }
            catch {

            }
        }

        public bool EscapeToTimeSpan(TimeSpan ts) {
            try {
                Player.Time = ts;
                return true;
            }
            catch {
                return false;
            }
        }

        public ImageSource GetImageSource(TimeSpan ts) {
            throw new NotImplementedException();
        }

        public bool Pause() {
            Player.Pause();
            return true;
        }

        public bool Play() {
            Player.Play();
            return true;
        }

        public bool Resume() {
            //Player.Resume();
            Player.Play();
            return true;
        }

        public bool SetVolume(uint val) {
            Player.Volume = (int) val;
            return true;
        }

        public bool Stop() {
            Player.Stop();
            return true;
        }
    }

    //播放文件项目;
    public class SongItem : BindableBase {
        public SongItem(IPlayer player) {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }
        public IPlayer Player { get; }

        /// <summary>
        /// 总时长;
        /// </summary>
        private TimeSpan? _totalLength;
        public TimeSpan? TotalLength {
            get {
                return _totalLength;
            }
            set {
                SetProperty(ref _totalLength, value);
            }
        }

        private bool _isChecked;
        public bool IsChecked {
            get {
                return _isChecked;
            }
            set {
                SetProperty(ref _isChecked, value);
            }
        }

        private ImageSource _preSource;
        public ImageSource PreSource {
            get {
                return _preSource;
            }
            set {
                SetProperty(ref _preSource, value);
            }
        }

        private string _songName;
        public string SongName {
            get {
                return _songName;
            }
            set {
                SetProperty(ref _songName, value);
            }
        }

        private TimeSpan? _songTimeLength;
        public TimeSpan? SongLength {
            get {
                return _songTimeLength;
            }
            set {
                SetProperty(ref _songTimeLength, value);
            }
        }

    }


}
