using SingularityForensic.Contracts.Casing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Casing {
    class RecentCaseRecord : IRecentCaseRecord {
        public string CaseName { get; internal set; }

        public string CasePath { get; internal set; }

        public string CaseTime { get; internal set; }
        
        public string CaseGUID { get; internal set; }
        
        public DateTime LastAccessTime { get; internal set; }
    }
}
