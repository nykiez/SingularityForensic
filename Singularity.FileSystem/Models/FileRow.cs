using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFC.Parse.IO;
using CDFCCultures.Helpers;
using System;
using System.IO;
using System.Windows.Media.Imaging;
using static CDFCCultures.Managers.ManagerLocator;
using EventLogger;
using Prism.Mvvm;
using CDFC.Parse.Android.Contracts;
using CDFC.Parse.Android.DeviceObjects;

namespace Singularity.UI.FileSystem.Models {
    public interface IHavePreviewSource {
        BitmapImage PreviewSource {get;}
    }

    public interface IFileRow<TFile> where TFile : IFile {
        TFile File { get; }
    }
    
    //目录/资源浏览器行模型;
    public class FileRow:BindableBase , IHavePreviewSource{
        private static BitmapImage dirImage;
        public static BitmapImage DirImage {
            get {
                if(dirImage == null) {
                    dirImage = new BitmapImage(
                        new Uri("pack://application:,,,/SingularityForensic;component/Images/Shell/Type_Directory.png", UriKind.Absolute)
                    );
                }
                return dirImage;
            }
        }

        private static BitmapImage regularImage;
        public static BitmapImage RegularImage {
            get {
                if(regularImage == null) {
                    regularImage = new BitmapImage(
                        new Uri("pack://application:,,,/SingularityForensic;component/Images/Shell/Type_RegularFile.ico", UriKind.Absolute)
                    );
                }
                return regularImage;
            }
        }
        
        /// <summary>
        /// 行模型的构造方法;
        /// </summary>
        /// <param name="file">构造所需的参数</param>
        public FileRow(IFile file) {
            this.File = file;
        }
        private string ConvertFileTypeToWord(FileType fType) {
            switch (fType) {
                case CDFC.Parse.Contracts.FileType.BlockDeviceFile:
                    if(File is Partition) {
                        switch((File as Partition).FSType) {
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

        public event EventHandler<bool> CheckChanged;
        public bool Checked {
            get {
                return isChecked;
            }
            set {
                SetProperty(ref isChecked, value);
                CheckChanged?.Invoke(this,true);
            }
        }
        private bool isChecked;
        public void NotifyChecked(bool val) => SetProperty(ref isChecked, val,nameof(Checked));
        public IFile File { get; private set; }
        
        public string FileName => File?.Name ?? string.Empty;                           //文件名;

        //private string _fileType;
        public string FileType => 
            ConvertFileTypeToWord(File?.FileType ?? CDFC.Parse.Contracts.FileType.Unknown);   //文件类型;

        private string _filePermission;
        public string FilePermission {
            get {
                if(_filePermission == null) {
                    if(File is IExt4Node) {
                        _filePermission =  FindResourceString((File as IExt4Node).GetPermission().ToString());
                    }
                }
                return _filePermission;
            }
        }

        //内部标识ID;
        private string _gid;
        public string GID {
            get {
                if(File is IExt4Node) {
                    _gid = (File as IExt4Node).GetUID().ToString();
                }
                return _gid;
            }
        }

        //标识ID;
        private string _uid;
        public string UID {
            get {
                if(File is IExt4Node) {
                    _uid = (File as IExt4Node).GetGID().ToString();
                }
                return _uid;
            }
        }

        public long FileSize => File?.Size ?? 0;                             //文件大小;
        private bool? _deleted;                                                     //是否被删除;
        public bool? Deleted {
            get {
                if(_deleted == null && File != null) {
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
        public DateTime? ModifiedTime => (File as ITimeable)?.ModifiedTime;  //最后修改时间;

        public DateTime? AccessedTime => (File as ITimeable)?.AccessedTime;                         //最后访问时间;        
        public DateTime? CreateTime => (File as ITimeable)?.CreateTime;                           //最后创建时间;    

        public string LastMountTime {
            get {
                if(File is AndroidPartition) {
                    return (File as AndroidPartition).LastMountTime.ToWDTimeString();
                }
                return string.Empty;
            }
        }

        public string LastWriteTime {
            get {
                if(File is AndroidPartition) {
                    return (File as AndroidPartition).LastWriteTime.ToWDTimeString();
                }
                return string.Empty;
            }
        }
        //private BitmapImage _previewSource;
        //public BitmapImage PreviewSource {
        //    get {
        //        if(_previewSource == null) {

        //        }
        //    }
        //}

        //起始扇区;
        //起始扇区;
        public long StartSec {
            get {
                if(File?.Name == "cover.jpg") {

                }
                if (File is Partition) {
                    return (File as Partition).StartLBA / File.GetParent<Device>()?.SecSize ?? 512;
                }
                else if (File is RegularFile || File is CDFC.Parse.Abstracts.Directory) {
                    var part = File.GetParent<Partition>();
                    if(part != null) {
                        if (File is RegularFile) {
                            return ((File as RegularFile).StartLBA + ( startLBALevel == 0 ? part.StartLBA : 0 )) 
                                / File.GetParent<Device>()?.SecSize ?? 512;
                        }
                        else if (File is CDFC.Parse.Abstracts.Directory) {
                            return ((File as CDFC.Parse.Abstracts.Directory).StartLBA + (startLBALevel == 1 ? part.StartLBA : 0)) 
                                / ( File.GetParent<Device>()?.SecSize ?? 512 );
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
            if(level == 1 || level == 2){
                startLBALevel = level;
                OnPropertyChanged(nameof(StartSec));
            }
        }

        public long EndSec {
            get {
                if(File is Partition) {
                    return ((File as Partition).EndLBA / File.GetParent<Device>()?.SecSize ?? 512);
                }
                return 0;
            }
        }

        //文件路径
        private string _filePath;
        public string FilePath => _filePath??(_filePath = File.GetFilePath());
       
        public int? PartitionIndex { get; set; }
        public static string GetRowTimeString(DateTime? dt) {
            return string.Format($"{dt?.Year}-{dt?.Month / 10}{dt?.Month % 10}-{dt?.Day / 10}{dt?.Day % 10} {dt?.Hour / 10}{dt?.Hour % 10}:{dt?.Minute / 10}{dt?.Minute % 10}:{dt?.Second / 10}{dt?.Second % 10}");
        }

        private static object sourceLocker = new object();
        private BitmapImage _previewSource;
        public BitmapImage PreviewSource {
            get {
                if(_previewSource == null){
                    if (File.FileType == CDFC.Parse.Contracts.FileType.Directory) {
                        _previewSource = DirImage;
                    }
                    else if(File.FileType == CDFC.Parse.Contracts.FileType.RegularFile) {
                        if((File.Name?.ToUpper().EndsWith("JPG")??false) 
                            ||(File.Name?.ToUpper().EndsWith("PNG")??false)) {
                            lock (sourceLocker) {
                                try {
                                    var stream = StreamExtensions.CreateStreamByFile(File);
                                    stream.Position = 0;
                                    var buffer = new byte[stream.Length];
                                    stream.Read(buffer, 0,(int) stream.Length);
                                    using(var ms = new MemoryStream(buffer)) {
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
                                catch(Exception ex) {
                                    Logger.WriteLine($"{nameof(FileRow)}->{nameof(PreviewSource)}:FileName{File.Name}-{ex.Message}");
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
    
    //public class FileRow<TFile> : FileRow, IFileRow<TFile> where TFile : IFile {
        
    //}
    
}
