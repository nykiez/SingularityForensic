using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Parse.Contracts {
    /// <summary>
    /// 分区表项;
    /// </summary>
    public class PartitionTableItem {
        //分区表项起始偏移;
        public long Offset { get; set; }
        //分区表项长度;
        public int Length { get; set; }
        
    }
}
