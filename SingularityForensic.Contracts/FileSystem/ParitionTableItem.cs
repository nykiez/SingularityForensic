using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    //分区表项信息;
    public class PartitionEntryStoken : SecurityStoken {
        //分区表起始长度;
        public long StartLBA { get; set; }
        //分区表项长度;
        public long Size { get; set; }
        //分区表项类型;
        public string TypeGUID { get; set; }
    }

    //分区表项;
    public class PartitionEntry : HaveStokenBase<PartitionEntryStoken> {
        public PartitionEntry(string key, PartitionEntryStoken stoken = null) : base(key, stoken) {

        }
        //分区表项起始偏移;
        public long StartLBA => _stoken?.StartLBA??-1;
        //分区表项长度;
        public long Length => _stoken?.Size??-1;

        public string TypeGUID => _stoken?.TypeGUID;
    }

}
