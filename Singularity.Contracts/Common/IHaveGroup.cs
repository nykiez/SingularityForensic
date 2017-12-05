using System.Collections.Generic;

namespace Singularity.Contracts.Common {
    public interface IHaveGroup<out TInstance> { 
        IEnumerable<TInstance> Members { get; }
    }
}
