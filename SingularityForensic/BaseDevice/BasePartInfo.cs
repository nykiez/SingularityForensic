using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.BaseDevice {
    /// <summary>
    /// //GPT/Dos分区信息基类;
    /// </summary>
    internal abstract class BasePartInfo {
        public StInFoDisk? StInFoDisk { get; set; }
    }

    /// <summary>
    /// //Dos分区项信息;
    /// </summary>
    internal class DOSPartInfo : BasePartInfo {
        public StDosPTable StDosPTable { get; set; }
    }

    /// <summary>
    /// //GPT分区项信息;
    /// </summary>
    internal class GPTPartInfo : BasePartInfo {
        public StGptPTable StGptPTable { get; set; }
        public StEFIInfo? StEFIInfo { get; set; }
        public StEFIPTable? StEFIPTable { get; set; }
    }
}
