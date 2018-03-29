using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.FileSystem {
    /// <summary>
    /// FAT分区表解析装置;
    /// </summary>
    [Export(typeof(IStreamParsingProvider))]
    partial class FatStreamParsingProvider : IStreamParsingProvider {
        public int Order => 32;

        public string GUID => Constants.StreamParser_FAT;

        public bool CheckIsValidStream(Stream stream) {
            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            try {
                var unManagedManager = new UnmanagedBasicDeviceManager(stream);
                var isFat = Partition_B_Fat(unManagedManager.BasicDevicePtr);
                unManagedManager.Dispose();
                return isFat;
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                return false;
            }
        }

        public FileBase ParseStream(Stream stream, string name, XElement xElem, ProgressReporter reporter) {
            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }
            //var stoken = new PartitonStoken {
            //    BaseStream = stream,
            //    BlockSize = 4096
            //}
            return new Partition(Constants.PartitionKey_FAT, null);
        }

        
    }

    /// <summary>
    /// 内部信息类;
    /// </summary>
    partial class FatStreamParsingProvider {
        /// <summary>
        /// FAT分区信息;
        /// </summary>
        internal class FATPartInfo {
            /// <summary>
            /// 引导扇区
            /// </summary>
            public StFatDBR StFatDBR { get; set; }
            /// <summary>
            /// 引导扇区备份;
            /// </summary>
            public StFatDBR StFatDBR_BackUp { get; set; }
            /// <summary>
            /// //FSINFO信息
            /// </summary>
            public StFatFSInfo StFatFSInfo { get; set; }
            /// <summary>
            /// //FSINFO信息备份
            /// </summary>
            public StFatFSInfo StFatFSInfo_BackUp { get; set; }
        }

        internal abstract class FATFileInfo {
            /// <summary>
            /// 文件节点;
            /// </summary>
            public StFileNode StFileNode { get; set; }
            public List<StClusterNode> StClusters { get; } = new List<StClusterNode>();
        }
    }

    partial class FatStreamParsingProvider {
        private const string partAsm = "PartitionManager.dll";
        [DllImport(partAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static bool Partition_B_Fat(IntPtr stPartition);

        private const string fatAsm = "FatRecover.dll";
        
        //StFileList* Fat_Get_RootDir(void* stPartition);
        [DllImport(fatAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Fat_Get_RootDir(IntPtr stPartition);

        //StFileList* Fat_Parse_Dir(void* stPartition, StClusterList* stCluster);
        [DllImport(fatAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Fat_Parse_Dir(IntPtr stPartition, IntPtr stCluster);
        
        //引导扇区
        //StFatFSDBR* Fat_Get_FsDBR(void* stPartition);
        [DllImport(fatAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Fat_Get_FsDBR(IntPtr stPartition);

        //引导扇区备份
        //StFatFSDBR* Fat_Get_BackupFsDBR(void* stPartition);
        [DllImport(fatAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Fat_Get_BackupFsDBR(IntPtr stPartition);

        //FSINFO信息
        //StFatFSInfo* Fat_Get_BackupFsInfo(void* stPartition);
        [DllImport(fatAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Fat_Get_BackupFsInfo(IntPtr stPartition);

        //FSINFO信息备份
        //StFatFSInfo* Fat_Get_FsInfo(void* stPartition);
        [DllImport(fatAsm, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr Fat_Get_FsInfo(IntPtr stPartition);

    }
}
