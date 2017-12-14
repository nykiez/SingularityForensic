using CDFC.Parse.Abstracts;
using CDFC.Parse.Android.Abstracts;
using CDFC.Parse.Android.Structs;
using CDFC.Parse.Contracts;
using EventLogger;

namespace CDFC.Parse.Android.DeviceObjects {
    //未知分区，尚未细分;
    public class AndroidUnknownParititon : TabPartPartition {
        /// <summary>
        /// 其他分区的构造方法;
        /// </summary>
        /// <param name="parent">父文件为设备</param>
        ///<param name="partTabInfo">分区结构（自定义）</param>
        public AndroidUnknownParititon(TabPartInfo partTabInfo, Device parent) : base(partTabInfo,parent) {   
            
        }

        public override uint ClusterSize => 0;

        public override FileSystemType FSType => FileSystemType.Unknown;

    }
}
