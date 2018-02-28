using System.Collections.Generic;

namespace SingularityForensic.Contracts.Info {
    public interface IHaveTalkLogs<out TTalklog> where TTalklog : ITalkLog {
        IEnumerable<TTalklog> TalkLogs { get; }
    }

    
}
