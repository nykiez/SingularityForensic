using System.Collections.Generic;

namespace Singularity.Interfaces {
    public interface IHaveGroup<out TData>  {
        IEnumerable<TData> Members { get; }
    }
}
