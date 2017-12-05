using CDFC.Parse.Abstracts;
using CDFC.Parse.Android.Structs;
using CDFC.Util.PInvoke;
using EventLogger;
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using CDFC.Parse.Contracts;

namespace CDFC.Parse.Android.DeviceObjects {
    /// <summary>
    /// 安卓设备;
    /// </summary>
    public class AndroidDevice:Device,IHandleDevice {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="H_Disk">传入对象句柄;</param>
        /// <returns> StDiskInfo *</returns>
        [DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Cflabqd_Init(SafeFileHandle H_Disk);
        [DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Cflabqd_Exit();
        [DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Cflab_Free_DiskInfo(IntPtr stDiskInfo);
        [DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Cflab_Free_All();

        private const string PartSuffix = "分区";
        /// <summary>
        /// 加载内容(子文件等);
        /// </summary>
        /// <param name="notifySizeAct">通知前台进度委托</param>
        /// <param name="curPartSize">已经读取到的当前分区的大小</param>
        public void LoadContent(Action<(long curPartSize,long thePartSize,
            long allReadSize,
            int allPart,int curPart)> notifySizeAct = null,
            Func<bool> isCancel = null) {
            try {
                if (MgrInfo != null) {
                    int partIndex = 1;
                    var allPart = MgrInfo.PartTabInfos.Count;

                    long allReadSize = 0;

                    MgrInfo.PartTabInfos.ForEach(p => {
                        Partition partition = null;

                        long curPartSize = 0;
                        //若FSType为3,则表示为Ext4分区;
                        if( p.StTabPartInfo.Value.FsType == 3) {
                            var adPartition = new AndroidPartition(p, this);
                            
                            adPartition.LoadChildren(sz => {
                                curPartSize += sz;
                                
                                notifySizeAct?.Invoke(
                                (curPartSize, adPartition.Size, allReadSize + curPartSize, allPart, partIndex));
                            },isCancel);

                            partition = adPartition;
                        }
                        else {
                            partition = new AndroidUnknownParititon(p, this);
                        }
                        //var name = Marshal.PtrToStringUni(p.StPartInfo.Value.PartTabNameUnicode, 36);
                        partition.Name = $"{PartSuffix}{(partIndex++)}({p.StPartInfo.Value.PartTabName})";
                        allReadSize += partition.Size;
                        notifySizeAct?.Invoke((partition.Size, partition.Size, allReadSize, allPart, partIndex));
                        Children.Add(partition);

                        if (isCancel?.Invoke() == true) {
                            return;
                        }
                    });
                }
            }
            catch(Exception ex) {
                Logger.WriteLine($"{nameof(AndroidDevice)}->{nameof(LoadContent)}:{ex.Message}");
            }
            
        }
        
        //[DllImport("cdfcqd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //private static extern IntPtr Cflabqd_Init(SafeFileHandle H_Disk);
        //public static Func<SafeFileHandle,IntPtr> CFLabAndroidInitFunc {
        //    get {
        //        return Cflabqd_Init;
        //      
        //}
        public AndroidDiskMgr MgrInfo { get;private set; }

        private Stream stream;                                      //设备流;
        public override Stream Stream {
            get {
                return stream;
            }
        }

        private SafeFileHandle handle;
        public SafeFileHandle Handle {
            get {
                if(handle == null && Stream != null) {
                    var fs = Stream as FileStream;
                    if(fs != null) {
                        handle = fs.SafeFileHandle;
                    }
                }
                return handle;
            }
        }
        
        /// <summary>
        /// 从路径中获得设备;
        /// </summary>
        /// <param name="path">读取路径名</param>
        /// <param name="ntfSizeAct">
        /// curSize:当前已经解析的分区大小;
        /// allSize:设备总大小;
        /// curPartSize:在单个分区中所读取的大小;
        /// thePartSize:单个分区大小;
        /// curPart:当前分区号;
        /// allPart:总共分区数目;
        /// </param>
        /// <returns></returns>
        [HandleProcessCorruptedStateExceptions]
        public static AndroidDevice LoadFromPath(string path,bool readOnly = true,
            Action<(
                long curSize,long allSize,
                long curPartSize,
                long thePartSize,
                int curPart,int allPart)> ntfSizeAct = null,
            Func<bool> isCancel = null) {

            FileStream fs = null;
            
            try {
                fs = File.Open(path, FileMode.Open, readOnly ? FileAccess.Read : FileAccess.ReadWrite, FileShare.ReadWrite);
                return LoadFromFileStream(fs,ntfSizeAct,isCancel);
            }
            catch(Exception ex) {
                Logger.WriteLine($"{nameof(AndroidDevice)} -> {nameof(LoadFromPath)}:{ex.Message}");
                fs?.Close();
                throw new Exception("Failed to load img!",ex);
            }
            
        }
        
        public static AndroidDevice LoadFromFileStream(FileStream fs,
            Action<(
                long curSize, long allSize,
                long curPartSize,
                long thePartSize,
                int curPart, int allPart)> ntfSizeAct = null,
            Func<bool> isCancel = null) {
            if(fs == null) {
                throw new ArgumentNullException(nameof(fs));
            }

            var ptr = Cflabqd_Init(fs.SafeFileHandle);

            var device = new AndroidDevice {
                stream = fs,
                Name = fs.Name.Substring(fs.Name.Replace("\\", "/").LastIndexOf("/") + 1)
            };

            #region 若读取分区表成功，则载入分区;
            if (ptr != IntPtr.Zero) {
                var stmgrInfo = ptr.GetStructure<StDiskMgr>();
                var stmbrInfo = stmgrInfo.MbrInfoPtr.GetStructure<StMbrInfo>();
                var stefiInfo = stmgrInfo.EFIInfoPtr.GetStructure<StEFIInfo>();
                var stPartPtr = stmgrInfo.PartTabInfoPtr;
                var stTabPartPtrNode = stPartPtr;

                device.MgrInfo = new AndroidDiskMgr {
                    EFIInfo = new AndroidEFIInfo {
                        StEFIInfo = stefiInfo
                    },
                    MBRInfo = new AndroidMbrInfo {
                        StMbrInfo = stmbrInfo
                    }
                };
                device._diskInfo = ptr;
                var count = 0;
                while (stTabPartPtrNode != IntPtr.Zero) {
                    var stTabPartInfo = stTabPartPtrNode.GetStructure<StTabPartInfo>();
                    //var stPartInfo = stTabPartInfo.PartInfoPtr.GetStructure<StAndroidPartInfo>();
                    var partTab = new TabPartInfo(stTabPartPtrNode);
                    device.MgrInfo.PartTabInfos.Add(partTab);
                    stTabPartPtrNode = stTabPartInfo.Next;
                    count++;
                }

            }
            #endregion
            else {
                fs.Close();
                Logger.WriteLine($"{nameof(AndroidDevice)}->{nameof(LoadFromPath)}:The init returned false");
                return null;
            }



            device.LoadContent(tuple => {
                ntfSizeAct?.Invoke(
                    (tuple.allReadSize, device.Stream.Length,
                    tuple.curPartSize, tuple.thePartSize,
                    tuple.curPart, tuple.allPart));
            }, isCancel);

            return device;
        }

        /// <summary>
        /// 检查是否是一个有效的安卓手机镜像;
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static bool CheckIsValid(FileStream fs) {
            if(fs == null) {
                throw new ArgumentNullException(nameof(fs));
            }

            try {
                var ptr = Cflabqd_Init(fs.SafeFileHandle);
                
                return ptr != IntPtr.Zero;
            }
            catch(Exception ex) {
                Logger.WriteCallerLine(ex.Message);
                return false;
            }
            
        }

        private int? secSize;                                                   //扇区大小;
        public override int SecSize {
            get {
                return secSize != null ? secSize.Value : 512;
            }
            protected set {
                secSize = value;
            }
        }

        private long? size;
        public override long Size {
            get {
                if(size == null) {
                    if(Stream != null) {
                        size = Stream.Length;
                    }
                }
                return size ?? 0;
            }
        }

        public override PartsType PartsType => PartsType.GPT;

        private IntPtr _diskInfo;

        public override void Exit() {
            Children.ForEach(p => {
                (p as AndroidPartition)?.Exit();
            });
            Stream.Close();
        }

        //全局释放方法;
        [HandleProcessCorruptedStateExceptions]
        public static bool FreeAll() {
            try {
                Cflab_Free_All();
                return true;
            }
            catch(Exception ex) {
                Logger.WriteLine($"{nameof(AndroidDevice)}->{nameof(Exit)}:{ex.Message}");
                return false;
            }
        }
               
    }

}
