using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFC.Parse.IO;
using System;
using System.IO;
using System.Windows.Media.Imaging;
using static CDFCCultures.Managers.ManagerLocator;
using EventLogger;
using Prism.Mvvm;
using Singularity.UI.FileSystem.Resources;

namespace Singularity.UI.FileSystem.Models {
    public interface IHavePreviewSource {
        BitmapImage PreviewSource {get;}
    }

    public interface IFileRow : IHavePreviewSource {
        event EventHandler<bool> CheckChanged;
        bool Checked { get; set; }
        void SetChecked(bool val);                       //设定选定状态而不触发事件;

        string FileName { get; }                           //文件名;

        //private string _fileType;
        //文件类型;
        string FileType { get; }  
        
        string FilePermission { get; }
        string GID { get; }

        //标识ID;
        string UID { get; }

        long FileSize { get; }                             //文件大小;
        bool? Deleted { get;  }
        FileType RowType { get; }         //行类型;
        DateTime? ModifiedTime {get;} //最后修改时间;
        DateTime? AccessedTime { get; }                         //最后访问时间;        
        DateTime? CreateTime { get; }                           //最后创建时间;    

        string LastMountTime { get; }

        string LastWriteTime { get;}

        //起始扇区;
        long StartSec { get; }
        
        long EndSec {get;}

        //文件路径
        string FilePath { get; }

        int? PartitionIndex { get; set; }
        
        /// <summary>
        /// 本地路径(若已经缓存);
        /// </summary>
        string LocalPath { get; set; }
        
    }

    public interface IFileRow<out TFile> : IFileRow where TFile : IFile {
        TFile File { get; }
    }
    
    public class FileRow<TFile> : BindableBase, IFileRow<TFile> where TFile : IFile {
        public static readonly BitmapImage DirImage = new BitmapImage(IconSources.DirectoryRowIcon);
        
        public static readonly BitmapImage RegularImage = new BitmapImage(IconSources.RegFileRowIcon);

        public FileRow(TFile file) {
            this.File = file;
        }

        public TFile File { get; }

        public event EventHandler<bool> CheckChanged;
        public bool Checked {
            get {
                return isChecked;
            }
            set {
                SetProperty(ref isChecked, value);
                CheckChanged?.Invoke(this, true);
            }
        }
        private bool isChecked;
        public virtual void SetChecked(bool val) => SetProperty(ref isChecked, val, nameof(Checked));

        public void NotifyChecked(bool val) => SetProperty(ref isChecked, val, nameof(Checked));

        public string FileName => File?.Name ?? string.Empty;                           //文件名;

        private string ConvertFileTypeToWord(FileType fType) {
            switch (fType) {
                case CDFC.Parse.Contracts.FileType.BlockDeviceFile:
                    if (File is Partition) {
                        switch ((File as Partition).FSType) {
                            case FileSystemType.Unknown:
                                return FindResourceString("UnKnown");
                            default:
                                return (File as Partition).FSType.ToString();
                        }
                    }
                    return "分区";
                case CDFC.Parse.Contracts.FileType.Directory:
                    return "目录";
                default:
                    return "文件";
            }
        }

        //private string _fileType;
        public virtual string FileType =>
            ConvertFileTypeToWord(File?.FileType ?? CDFC.Parse.Contracts.FileType.Unknown);   //文件类型;
        
        public virtual string FilePermission => string.Empty;

        //内部标识ID;
        public virtual string GID => string.Empty;

        //标识ID;
        public virtual string UID => string.Empty;

        public long FileSize => File?.Size ?? 0;                             //文件大小;
        private bool? _deleted;                                                     //是否被删除;
        public virtual bool? Deleted {
            get {
                if (_deleted == null && File != null) {
                    if (File.FileType == CDFC.Parse.Contracts.FileType.Directory) {
                        _deleted = (File as CDFC.Parse.Abstracts.Directory)?.Deleted;
                    }
                    else if (File.FileType == CDFC.Parse.Contracts.FileType.RegularFile) {
                        _deleted = (File as RegularFile)?.Deleted;
                    }
                    else if (File is OtherFile) {
                        _deleted = (File as OtherFile)?.Deleted;
                    }
                }
                return _deleted;
            }
        }
        public FileType RowType => File?.FileType ?? CDFC.Parse.Contracts.FileType.Unknown;         //行类型;
        public virtual DateTime? ModifiedTime => (File as ITimeable)?.ModifiedTime;  //最后修改时间;

        public virtual DateTime? AccessedTime => (File as ITimeable)?.AccessedTime;                         //最后访问时间;        
        public virtual DateTime? CreateTime => (File as ITimeable)?.CreateTime;                           //最后创建时间;    

        public virtual string LastMountTime => string.Empty;

        public virtual string LastWriteTime => string.Empty;

        //起始扇区;
        public virtual long StartSec {
            get {
                if (File is Partition) {
                    return (File as Partition).StartLBA / File.GetParent<Device>()?.SecSize ?? 512;
                }
                else if (File is RegularFile || File is CDFC.Parse.Abstracts.Directory) {
                    var part = File.GetParent<Partition>();
                    if (part != null) {
                        if (File is RegularFile) {
                            return ((File as RegularFile).StartLBA + (startLBALevel == 0 ? part.StartLBA : 0))
                                / File.GetParent<Device>()?.SecSize ?? 512;
                        }
                        else if (File is CDFC.Parse.Abstracts.Directory) {
                            return ((File as CDFC.Parse.Abstracts.Directory).StartLBA + (startLBALevel == 1 ? part.StartLBA : 0))
                                / (File.GetParent<Device>()?.SecSize ?? 512);
                        }
                    }
                }
                return 0;
            }
        }

        private int startLBALevel;
        /// <summary>
        /// 调整文件起始偏移相对;
        /// </summary>
        /// <param name="level">1为相对设备，2为相对分区</param>
        public void SwitchFileStartSecTo(int level) {
            if (level == 1 || level == 2) {
                startLBALevel = level;
                RaisePropertyChanged(nameof(StartSec));
            }
        }

        public virtual long EndSec {
            get {
                if (File is Partition) {
                    return ((File as Partition).EndLBA / File.GetParent<Device>()?.SecSize ?? 512);
                }
                return 0;
            }
        }

        //文件路径
        private string _filePath;
        public virtual string FilePath => _filePath ?? (_filePath = File.GetFilePath());

        public int? PartitionIndex { get; set; }
        public static string GetRowTimeString(DateTime? dt) {
            return string.Format($"{dt?.Year}-{dt?.Month / 10}{dt?.Month % 10}-{dt?.Day / 10}{dt?.Day % 10} {dt?.Hour / 10}{dt?.Hour % 10}:{dt?.Minute / 10}{dt?.Minute % 10}:{dt?.Second / 10}{dt?.Second % 10}");
        }

        private static object sourceLocker = new object();
        private BitmapImage _previewSource;
        public virtual BitmapImage PreviewSource {
            get {
                if (_previewSource == null) {
                    if (File.FileType == CDFC.Parse.Contracts.FileType.Directory) {
                        _previewSource = DirImage;
                    }
                    else if (File.FileType == CDFC.Parse.Contracts.FileType.RegularFile) {
                        if ((File.Name?.ToUpper().EndsWith("JPG") ?? false)
                            || (File.Name?.ToUpper().EndsWith("PNG") ?? false)) {
                            lock (sourceLocker) {
                                try {
                                    var stream = StreamExtensions.CreateStreamByFile(File);
                                    stream.Position = 0;
                                    var buffer = new byte[stream.Length];
                                    stream.Read(buffer, 0, (int)stream.Length);
                                    using (var ms = new MemoryStream(buffer)) {
                                        _previewSource = new BitmapImage();
                                        _previewSource.BeginInit();
                                        _previewSource.CacheOption = BitmapCacheOption.OnLoad;
                                        _previewSource.StreamSource = ms;
                                        _previewSource.DecodePixelWidth = 400;
                                        _previewSource.EndInit();
                                        _previewSource.Freeze();
                                    };
                                    stream.Close();
                                }
                                catch (Exception ex) {
                                    Logger.WriteCallerLine(ex.Message);
                                    _previewSource = RegularImage;
                                }
                            }
                        }
                        else {
                            _previewSource = RegularImage;
                        }
                    }
                }

                return _previewSource;
            }
        }

        /// <summary>
        /// 本地路径(若已经缓存);
        /// </summary>
        public string LocalPath { get; set; }
    }

    ////目录/资源浏览器行模型;
    //public class FileRow:BindableBase , IHavePreviewSource{
    //    /// <summary>
    //    /// 行模型的构造方法;
    //    /// </summary>
    //    /// <param name="file">构造所需的参数</param>
    //    public FileRow(IFile file) {
    //        this.File = file;
    //    }
    //    private string ConvertFileTypeToWord(FileType fType) {
    //        switch (fType) {
    //            case CDFC.Parse.Contracts.FileType.BlockDeviceFile:
    //                if(File is Partition) {
    //                    switch((File as Partition).FSType) {
    //                        case FileSystemType.Unknown:
    //                            return FindResourceString("UnKnown");
    //                        default:
    //                            return (File as Partition).FSType.ToString();
    //                    }
    //                }
    //                return "分区";
    //            case CDFC.Parse.Contracts.FileType.Directory:
    //                return "目录";
    //            default:
    //                return "文件";
    //        }
    //    }

    //    public event EventHandler<bool> CheckChanged;
    //    public bool Checked {
    //        get {
    //            return isChecked;
    //        }
    //        set {
    //            SetProperty(ref isChecked, value);
    //            CheckChanged?.Invoke(this,true);
    //        }
    //    }
    //    private bool isChecked;
    //    public void SetChecked(bool val) => SetProperty(ref isChecked, val,nameof(Checked));
    //    public IFile File { get; private set; }
        
    //    public string FileName => File?.Name ?? string.Empty;                           //文件名;

    //    //private string _fileType;
    //    public string FileType => 
    //        ConvertFileTypeToWord(File?.FileType ?? CDFC.Parse.Contracts.FileType.Unknown);   //文件类型;


    //    public long FileSize => File?.Size ?? 0;                             //文件大小;
    //    private bool? _deleted;                                                     //是否被删除;
    //    public bool? Deleted {
    //        get {
    //            if(_deleted == null && File != null) {
    //                if (File.FileType == CDFC.Parse.Contracts.FileType.Directory) {
    //                    _deleted = (File as CDFC.Parse.Abstracts.Directory)?.Deleted;
    //                }
    //                else if (File.FileType == CDFC.Parse.Contracts.FileType.RegularFile) {
    //                    _deleted = (File as RegularFile)?.Deleted;
    //                }
    //                else if (File is OtherFile) {
    //                    _deleted = (File as OtherFile)?.Deleted;
    //                }
    //            }
    //            return _deleted;
    //        }
    //    }
    //    public FileType RowType => File?.FileType ?? CDFC.Parse.Contracts.FileType.Unknown;         //行类型;
    //    public DateTime? ModifiedTime => (File as ITimeable)?.ModifiedTime;  //最后修改时间;

    //    public DateTime? AccessedTime => (File as ITimeable)?.AccessedTime;                         //最后访问时间;        
    //    public DateTime? CreateTime => (File as ITimeable)?.CreateTime;                           //最后创建时间;    

    

    
    //    //起始扇区;
    //    public long StartSec {
    //        get {
    //            if(File?.Name == "cover.jpg") {

    //            }
    //            if (File is Partition) {
    //                return (File as Partition).StartLBA / File.GetParent<Device>()?.SecSize ?? 512;
    //            }
    //            else if (File is RegularFile || File is CDFC.Parse.Abstracts.Directory) {
    //                var part = File.GetParent<Partition>();
    //                if(part != null) {
    //                    if (File is RegularFile) {
    //                        return ((File as RegularFile).StartLBA + ( startLBALevel == 0 ? part.StartLBA : 0 )) 
    //                            / File.GetParent<Device>()?.SecSize ?? 512;
    //                    }
    //                    else if (File is CDFC.Parse.Abstracts.Directory) {
    //                        return ((File as CDFC.Parse.Abstracts.Directory).StartLBA + (startLBALevel == 1 ? part.StartLBA : 0)) 
    //                            / ( File.GetParent<Device>()?.SecSize ?? 512 );
    //                    }
    //                }
    //            }
    //            return 0;
    //        }
    //    }
        
    //    private int startLBALevel;
    //    /// <summary>
    //    /// 调整文件起始偏移相对;
    //    /// </summary>
    //    /// <param name="level">1为相对设备，2为相对分区</param>
    //    public void SwitchFileStartSecTo(int level) {
    //        if(level == 1 || level == 2){
    //            startLBALevel = level;
    //            RaisePropertyChanged(nameof(StartSec));
    //        }
    //    }

    //    public long EndSec {
    //        get {
    //            if(File is Partition) {
    //                return ((File as Partition).EndLBA / File.GetParent<Device>()?.SecSize ?? 512);
    //            }
    //            return 0;
    //        }
    //    }

    //    //文件路径
    //    private string _filePath;
    //    public string FilePath => _filePath??(_filePath = File.GetFilePath());
       
    //    public int? PartitionIndex { get; set; }
    //    public static string GetRowTimeString(DateTime? dt) {
    //        return string.Format($"{dt?.Year}-{dt?.Month / 10}{dt?.Month % 10}-{dt?.Day / 10}{dt?.Day % 10} {dt?.Hour / 10}{dt?.Hour % 10}:{dt?.Minute / 10}{dt?.Minute % 10}:{dt?.Second / 10}{dt?.Second % 10}");
    //    }

    //    private static object sourceLocker = new object();
    //    private BitmapImage _previewSource;
    //    public BitmapImage PreviewSource {
    //        get {
    //            if(_previewSource == null){
    //                if (File.FileType == CDFC.Parse.Contracts.FileType.Directory) {
    //                    _previewSource = DirImage;
    //                }
    //                else if(File.FileType == CDFC.Parse.Contracts.FileType.RegularFile) {
    //                    if((File.Name?.ToUpper().EndsWith("JPG")??false) 
    //                        ||(File.Name?.ToUpper().EndsWith("PNG")??false)) {
    //                        lock (sourceLocker) {
    //                            try {
    //                                var stream = StreamExtensions.CreateStreamByFile(File);
    //                                stream.Position = 0;
    //                                var buffer = new byte[stream.Length];
    //                                stream.Read(buffer, 0,(int) stream.Length);
    //                                using(var ms = new MemoryStream(buffer)) {
    //                                    _previewSource = new BitmapImage();
    //                                    _previewSource.BeginInit();
    //                                    _previewSource.CacheOption = BitmapCacheOption.OnLoad;
    //                                    _previewSource.StreamSource = ms;
    //                                    _previewSource.DecodePixelWidth = 400;
    //                                    _previewSource.EndInit();
    //                                    _previewSource.Freeze();
    //                                };
    //                                stream.Close();
    //                            }
    //                            catch(Exception ex) {
    //                                Logger.WriteLine($"{nameof(FileRow)}->{nameof(PreviewSource)}:FileName{File.Name}-{ex.Message}");
    //                                _previewSource = RegularImage;
    //                            }
    //                        }
    //                    }
    //                    else {
    //                        _previewSource = RegularImage;
    //                    }
    //                }
    //            }

    //            return _previewSource;
    //        }
    //    }

    //    /// <summary>
    //    /// 本地路径(若已经缓存);
    //    /// </summary>
    //    public string LocalPath { get; set; }
        
    //}
    
    //public class FileRow<TFile> : FileRow, IFileRow<TFile> where TFile : IFile {
        
    //}
    
}
