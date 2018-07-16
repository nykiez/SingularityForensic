using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.ITunes {
    /// <summary>
    /// ITunes备份管理单位;
    /// </summary>
    public class ITunesBackUpManager {
        /// <summary>
        /// 实际存储的内容;
        /// </summary>
        public IDirectory Directory { get; internal set; }
        public StIOSBasicInfo? BasicInfo { get; internal set; }
    }
}
