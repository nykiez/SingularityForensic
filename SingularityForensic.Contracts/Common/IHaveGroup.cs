using System.Collections.Generic;

namespace SingularityForensic.Contracts.Common {
    public interface IHaveGroup<out TInstance> { 
        IEnumerable<TInstance> Members { get; }
    }
}
