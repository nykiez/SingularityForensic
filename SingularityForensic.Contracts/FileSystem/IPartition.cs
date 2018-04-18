using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileSystem {
    /// <summary>
    /// 分区类型;
    /// </summary>
    public interface IPartitionType {
        string PartTypeName { get; }
        string GUID { get; }
    }

    public class PartitionStoken : StreamFileStoken {
        public IPartitionType PartType { get; set; }
    }
    
    public interface IPartition: IStreamFile<PartitionStoken>{
        IPartitionType PartType { get; }
    }
}
