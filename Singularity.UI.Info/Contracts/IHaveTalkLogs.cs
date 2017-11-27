using System.Collections.Generic;

namespace Singularity.UI.Info.Contracts {
    public interface IHaveTalkLogs<out TTalklog> where TTalklog : ITalkLog {
        IEnumerable<TTalklog> TalkLogs { get; }
    }

    
}
