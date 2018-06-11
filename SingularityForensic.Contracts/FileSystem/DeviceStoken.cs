using CDFC.Util.IO;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.FileSystem {
    public class DeviceStoken : StreamFileStoken {
        public string PartsType { get; set; }                    //分区表类型;
        public IList<IPartitionEntry> PartitionEntries { get; } = new List<IPartitionEntry>(); //分区表项集合;
    }

    

   

    
}
