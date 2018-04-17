using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FAT {
    /// <summary>
    /// FAT分区信息;
    /// </summary>
    class FATPartInfo {
        /// <summary>
        /// 非托管状态;
        /// </summary>
        public UnmanagedFATManager UnmanagedFATManager { get; set; }
        /// <summary>
        /// 引导扇区
        /// </summary>
        public (StFatFSDBR? fsDbr, StFatDBR? dbr) FatDBR { get; set; }
        /// <summary>
        /// 引导扇区备份;
        /// </summary>
        public (StFatFSDBR? fsDbr, StFatDBR? dbr) FatDBR_BackUp { get; set; }
        /// <summary>
        /// //FSINFO信息
        /// </summary>
        public (StFatFSInfo fatFsInfo, StFatINFO? fsInfo)? FatInfo { get; set; }
        /// <summary>
        /// //FSINFO信息备份
        /// </summary>
        public (StFatFSInfo fatFsInfo, StFatINFO? fsInfo)? FatInfo_BackUp { get; set; }


        /// <summary>
        /// 簇大小;
        /// </summary>
        private int? _clusterSize;
        public int? ClusterSize {
            get {
                if (_clusterSize == null) {
                    if (FatDBR.dbr != null) {
                        _clusterSize = FatDBR.dbr.Value.ClusterSize;
                    }
                    else if (FatDBR_BackUp.dbr != null) {
                        _clusterSize = FatDBR.dbr.Value.ClusterSize;
                    }
                }
                return _clusterSize;
            }
        }
    }

    
}
