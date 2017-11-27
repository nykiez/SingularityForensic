using Singularity.UI.Info.Contracts;

namespace Singularity.UI.AdbViewer.Contracts {
    //单纯信息载体接口;
    public interface IInfoModelContainer {
        MInfoType InfoType { get; }
    }
}
