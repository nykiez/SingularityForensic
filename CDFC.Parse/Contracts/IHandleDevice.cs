using Microsoft.Win32.SafeHandles;

namespace CDFC.Parse.Contracts {
    public interface IHandleDevice {
        SafeFileHandle Handle { get; }
    }
}
