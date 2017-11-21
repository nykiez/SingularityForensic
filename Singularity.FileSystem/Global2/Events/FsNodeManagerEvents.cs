using CDFCUIContracts.Models;
using Prism.Events;

namespace SingularityForensic.Modules.FileSystem.Global.Events {
    //当文件系统选择的树形节点发生了变化时发生;
    public class FsSelectedUnitChangedEvent : PubSubEvent<ITreeUnit> {
        
    }

    //当文件系统树形任意节点被左击时发生;
    public class FsUnitClickEvent : PubSubEvent<ITreeUnit> {

    }
    
    //当文件系统树形(主分支)被附加节点时发生;
    public class FsUnitAdded : PubSubEvent<ITreeUnit> {

    }


}
