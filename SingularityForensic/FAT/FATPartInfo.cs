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
        public FATDBR FatDBR { get; set; }
        /// <summary>
        /// 引导扇区备份;
        /// </summary>
        public FATDBR FatDBR_BackUp { get; set; }
        /// <summary>
        /// //FSINFO信息
        /// </summary>
        public FATInfo FatInfo { get; set; }
        /// <summary>
        /// //FSINFO信息备份
        /// </summary>
        public FATInfo FatInfo_BackUp { get; set; }


        /// <summary>
        /// 簇大小;
        /// </summary>
        private int? _clusterSize;
        public int? ClusterSize {
            get {
                if (_clusterSize == null) {
                    if (FatDBR != null) {
                        _clusterSize = FatDBR.StructInstance.ClusterSize;
                    }
                    else if (FatDBR_BackUp != null) {
                        _clusterSize = FatDBR.StructInstance.ClusterSize;
                    }
                }
                return _clusterSize;
            }
        }
    }

    
}
