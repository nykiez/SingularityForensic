using SingularityForensic.Contracts.Info;

namespace SingularityForensic.Adb.Contracts {
    //单纯信息载体接口;
    public interface IInfoModelContainer {
        MInfoType InfoType { get; }
    }
}
