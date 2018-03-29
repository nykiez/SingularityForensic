﻿using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// //分区表项信息;
    /// </summary>
    public class PartitionEntryStoken : SecurityStoken {
        /// <summary>
        /// //分区表起始偏移;
        /// </summary>
        public long StartLBA { get; set; }
        /// <summary>
        /// //分区表项长度;
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 分区起始位移;
        /// </summary>
        public long PartStartLBA { get; set; }
        /// <summary>
        /// 分区大小;
        /// </summary>
        public long PartSize { get; set; }
        
        /// <summary>
        /// 分区表项类型;
        /// </summary>
        public string TypeGUID { get; set; }
    }

    /// <summary>
    /// 分区表项;
    /// </summary>
    public class PartitionEntry : HaveStokenBase<PartitionEntryStoken> {
        public PartitionEntry(string key, PartitionEntryStoken stoken = null) : base(key, stoken) {
            
        }

        /// <summary>
        /// 分区表项起始偏移;
        /// </summary>
        public long StartLBA => _stoken?.StartLBA??-1;
        /// <summary>
        /// 分区表项长度;
        /// </summary>
        public long Size => _stoken?.Size ?? -1;

        /// <summary>
        /// 分区起始位移;
        /// </summary>
        public long PartStartLBA => _stoken.PartStartLBA;
        /// <summary>
        /// 分区大小;
        /// </summary>
        public long PartSize => _stoken.PartSize;
        
        //分区表项类型;
        public string TypeGUID => _stoken?.TypeGUID;
    }

}