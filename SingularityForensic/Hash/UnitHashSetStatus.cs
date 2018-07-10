using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash {
    [Serializable]
    public class UnitHashSetStatus : IUnitHashSetStatus {
        public UnitHashSetStatus(string unitName) {
            this.UnitName = unitName ?? throw new ArgumentNullException(nameof(unitName));
        }

        /// <summary>
        /// 哈希集集合的GUID;
        /// </summary>
        public IEnumerable<string> HashSetGuids { get; internal set; }

        public string UnitName { get; }

        public string StatusType { get;
#if !DEBUG
        internal
#endif
            set;
        }
        
    }
}
