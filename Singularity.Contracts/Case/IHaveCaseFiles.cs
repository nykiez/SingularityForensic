using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.Contracts.Case {
    public interface IHaveCaseFiles {
        //子案件文件(针对分区,分区间隙);
        IEnumerable<ICaseEvidence> InnerCaseFiles { get; }
    }
}
