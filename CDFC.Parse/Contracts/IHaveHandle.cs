using Microsoft.Win32.SafeHandles;

namespace CDFC.Parse.Contracts {
    public interface IHaveHandle {
        SafeFileHandle Handle { get; }
    }
}
