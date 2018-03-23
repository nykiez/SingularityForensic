using Microsoft.Win32.SafeHandles;
using System;
using System.IO;

namespace SingularityForensic.Drive {
    /// <summary>
    /// 本地盘符
    /// </summary>
    public partial class LocalVolume {
        public LocalVolume(VolumeStruct st) {
            LoGo = st.m_LoGo;
            VolumeName = st.VolumeName;
            FileSystem = st.FileSystem;
            Name = st.m_Name;
            Size = (long)st.m_Size;
            Offset = st.m_Offset;
            Boot = st.m_Boot;
            pDev = st.m_pDev;
            Sign = Convert.ToChar(st.m_Sign);
            pDBR = st.pDBR;
        }
        public long Size { get; }
        public int LoGo { get;  }   //为那个物理设备的分区
        public string VolumeName { get; }  //卷标名称
        public string FileSystem { get; }     //文件系统
        public IntPtr Name { get;  }              //分区名称
        public ulong Offset { get;  }                  //MBR的偏移
        public bool Boot { get;  }                      //是否引导
        public IntPtr pDev { get; }                 //指向设备
        public char Sign { get; }                      //分区盘符
        public IntPtr pDBR { get;  }
        public HddInfo HddInfo { get;  }

        //剩余空间;
        private long? freeSpace;
        public long FreeSpace {
            get {
                if (!Char.IsLetter(Sign)) {
                    return 0;
                }

                if (freeSpace == null) {
                    try {
                        DriveInfo di = new DriveInfo(Sign.ToString());
                        freeSpace = di.AvailableFreeSpace;
                    }
                    catch (Exception ex) {
                        EventLogger.Logger.WriteLine("获得剩余空间失败!" + ex.Message);
                        freeSpace = 0;
                    }
                }
                return freeSpace.Value;
            }
        }

        //已用空间;
        public long ElapsedSpace {
            get {
                return Size - FreeSpace;
            }
        }

        private DriveStream _stream;
        public Stream GetStream() {
            if(_stream == null) {
                _stream = new DriveStream(Handle,this.SecSize) {
                    InternalLength = this.Size
                };
            }
            return _stream;
        }
    }

    //本程序集内部可修改部分;
    public partial class LocalVolume {
        internal int InternalSecSize { get; set; }
        public int SecSize => InternalSecSize;

        internal SafeFileHandle InternalHandle { get; set; }
        public SafeFileHandle Handle => InternalHandle;
    }
}
