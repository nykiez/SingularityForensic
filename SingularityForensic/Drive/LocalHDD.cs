using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SingularityForensic.Drive {
    /// <summary>
    /// 本地HDD(硬盘)存储介质,用作于硬盘描述信息;
    /// </summary>
    public partial class LocalHDD  {
        public LocalHDD(PhysicsDeviceStruct st) {
            DeviceID = st.ObjectID;
            Lable = st.Lable;
            DevName = st.DevName;
            Size = (long)st.DevSize;
            DevCHS = CHS.Create(st.DevCHS);
            DevMomd = st.DevMomd;
            DevType = st.DevType;
            SectorSize = st.SectorSize;
            Buffer = st.Buffer;
            Partition = st.Partiton;
            Arch = st.Arch;
            DevRW = st.DevRW;
            DevState = st.DevState;
            InternalVolumes = new List<LocalVolume>();

            if(st.Handle != IntPtr.Zero) {
                Handle = new SafeFileHandle(st.Handle,false);
            }
        }

        public int DeviceID { get;  }                     //设备标数(如果为16不以物理名称打开)
        public string Lable { get;  }
        public string DevName { get;  }                  //驱动名称
        public long Size { get; }
        public int SectorSize { get; }
        public CHS DevCHS { get;  }                   //设备几何
        public ulong DevMomd { get; }                      //访问模式
        public uint DevType { get; }                   //设备类型
        public SafeFileHandle Handle { get; }
        public IntPtr Buffer { get; }                     //读写缓存
        public IntPtr Partition { get;  }                    //分区结构（废弃）
        public IntPtr Arch { get; }                   //调用指针
        public IntPtr DevRW { get;  }                      //设备读写
        public bool DevState { get;  }

        /// <summary>
        /// 硬盘标识号;
        /// </summary>
        public string SerialNumber {
            get {
                if (HddInfo?.HddInfo2 != null) {
                    return HddInfo.HddInfo2.ModelNumber;
                }
                return HddInfo?.VendorID ?? string.Empty;
            }
        }

        private DriveStream _stream;
        public Stream GetStream() {
            if(_stream == null) {
                _stream = new DriveStream(Handle,this.SectorSize);
                _stream.InternalLength = this.Size;
            }
            return _stream;
        }
    }
    
    //本程序集内部可修改部分;
    public partial class LocalHDD {
        internal HddInfo InternalHDDInfo { get; set; }
        public HddInfo HddInfo => InternalHDDInfo;
        internal List<LocalVolume> InternalVolumes { get; } = new List<LocalVolume>();
        public IEnumerable<LocalVolume> Volumes => InternalVolumes.Select(p => p);
    }
}
