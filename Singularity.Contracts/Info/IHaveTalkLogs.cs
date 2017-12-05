using System.Collections.Generic;

namespace Singularity.Contracts.Info {
    public interface IHaveTalkLogs<out TTalklog> where TTalklog : ITalkLog {
        IEnumerable<TTalklog> TalkLogs { get; }
    }

    
}
