using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing {
    /// <summary>
    /// 最近打开案件记录;
    /// </summary>
    public interface IRecentCaseRecord {
        string CaseName { get; }
        string CasePath { get; }
        string CaseTime { get; }
        string CaseGUID { get; }
        /// <summary>
        /// 上次打开时间;
        /// </summary>
        DateTime LastAccessTime { get; }

    }

    
}
