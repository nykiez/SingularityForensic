using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFC.Parse.Android.Structs;
using EventLogger;
namespace CDFC.Parse.Android.DeviceObjects {
    //其他分区，尚未细分;
    public class AndroidOtherParititon : Partition {
        /// <summary>
        /// 其他分区的构造方法;
        /// </summary>
        /// <param name="parent">父文件为设备</param>
        ///<param name="partTabInfo">分区结构（自定义）</param>
        public AndroidOtherParititon(TabPartInfo partTabInfo, Device parent):base(parent) {
            this.TabPartInfo = partTabInfo;
            LoadContent();
        }
        private TabPartInfo TabPartInfo;

        /// <summary>
        /// 加载分区信息;（起始，终止LBA等);
        /// </summary>
        private void LoadContent() {
            if(TabPartInfo != null && TabPartInfo.StPartInfo != null) {
                var device = Parent as Device;
                if(device != null) {
                    #region 起始/扇区偏移量 = StartLBA / EndLBA * 设备扇区大小;
                    if (TabPartInfo.FSType != FileSystemType.EXT4) {
                        StartLBA = (long)TabPartInfo.StPartInfo.Value.PartTabStartLBA * device.SecSize;
                        EndLBA = (long)TabPartInfo.StPartInfo.Value.PartTabEndLBA * device.SecSize;
                    }
                    else {
                        Logger.WriteLine($"{nameof(AndroidOtherParititon)}->{nameof(LoadContent)}:Wrong FsType-{TabPartInfo.FSType}");
                        return;
                    }
                    #endregion
                }
                else {
                    Logger.WriteLine($"{nameof(AndroidOtherParititon)}->{nameof(LoadContent)}:Failed to convert to Device.");
                }
            }
            else {
                Logger.WriteLine($"{nameof(AndroidOtherParititon)}->{nameof(LoadContent)}:value can't be null-{nameof(TabPartInfo)}");
            }
        }
        public StTabPartInfo? StTabPartInfo { get; private set; }

        public override uint? BlockSize {
            get {
                return null;
            }
        }

        public override FileSystemType FSType {
            get {
                if (TabPartInfo != null) {
                    return TabPartInfo.FSType;
                }
                return FileSystemType.Unknown;
            }
        }
        
    }
}
