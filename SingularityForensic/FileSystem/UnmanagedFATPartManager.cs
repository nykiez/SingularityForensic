using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {
    /// <summary>
    /// FAT信息管理器,用于处理非托管的状态保存;
    /// </summary>
    public class UnmanagedFATPartManager {
        /// <summary>
        /// 非托管单元指针;
        /// </summary>
        public IntPtr FatPtr { get; set; }

        /// <summary>
        /// 流适配器实例;
        /// </summary>
        public UnmanagedStreamAdapter StreamAdpater { get; set; }
    }
}
