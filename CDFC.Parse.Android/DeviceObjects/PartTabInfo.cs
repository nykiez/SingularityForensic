using CDFC.Parse.Contracts;
using CDFC.Parse.Android.Structs;
using CDFC.Util.PInvoke;
using System;
using EventLogger;
using System.Collections.Generic;

namespace CDFC.Parse.Android.DeviceObjects {
    /// <summary>
    /// 分区类型(自定义);
    /// </summary>
    public class TabPartInfo {
        /// <summary>
        /// 分区类型（自定义)的构造方法;
        /// </summary>
        /// <param name="stTabPartInfoPtr">StTabPartInfo的非托管指针</param>
        public TabPartInfo(IntPtr stTabPartInfoPtr) {
            if(stTabPartInfoPtr == IntPtr.Zero) {
                throw new ArgumentException(nameof(stTabPartInfoPtr));
            }

            this.StTabPartInfoPtr = stTabPartInfoPtr;
            LoadInfo();
        }

        private void LoadInfo() {
            try {
                if(StTabPartInfoPtr != IntPtr.Zero) {
                    StTabPartInfo = StTabPartInfoPtr.GetStructure<StTabPartInfo>();
                }
            }
            catch(Exception ex) {
                Logger.WriteLine($"{nameof(TabPartInfo)}->{nameof(LoadInfo)}:{ex.Message}");
            }
        }
        //分区结构(自定义)指针;
        public IntPtr StTabPartInfoPtr { get;private set; } = IntPtr.Zero;

        public StTabPartInfo? StTabPartInfo { get; set; }

        private StPartInfo? stPartInfo;
        public StPartInfo? StPartInfo {
            get {
                if(stPartInfo == null && StTabPartInfo != null) {
                    stPartInfo = StTabPartInfo.Value.PartInfoPtr.GetStructure<StPartInfo>();
                }
                return stPartInfo;
            }
        }

        private AndroidSuperBlock superBlockInfo;
        public AndroidSuperBlock SuperBlockInfo {
            get {
                if(superBlockInfo == null && StTabPartInfo != null) {
                    superBlockInfo = new AndroidSuperBlock {
                        StSuperBlock = StTabPartInfo.Value.Ext4SuperBlock.GetStructure<StSuperBlock>()
                    };
                }
                return superBlockInfo;
            }
        }

        private List<StExt4GroupDesc?> _ext4GroupDecs;
        public List<StExt4GroupDesc?> Ext4GroupDecs {
            get {
                if (_ext4GroupDecs == null) {
                    _ext4GroupDecs = new List<StExt4GroupDesc?>();
                    var groupPtr = StTabPartInfo.Value.Ext4GroupDesc;
                    while(groupPtr != IntPtr.Zero) {
                        var group = groupPtr.GetStructure<StExt4GroupDesc>();
                        _ext4GroupDecs.Add(group);
                        groupPtr = group.Next;
                    }
                }
                return _ext4GroupDecs;
            }
        }
        
        public FileSystemType FSType {
            get {
                if(StTabPartInfo != null) {
                    switch (StTabPartInfo.Value.FsType) {
                        case FsType.FAT16:
                            return FileSystemType.FAT16;
                        case FsType.FAT32:
                            return FileSystemType.FAT32;
                        case FsType.EXT4:
                            return FileSystemType.EXT4;
                        default:
                            break;
                    }
                }
                return FileSystemType.Unknown;
            }
        }
    }
}
