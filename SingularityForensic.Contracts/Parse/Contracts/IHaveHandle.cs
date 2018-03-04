using Microsoft.Win32.SafeHandles;

namespace SingularityForensic.Contracts.Parse.Contracts {
    public interface IHaveHandle {
        SafeFileHandle Handle { get; }
    }
}
