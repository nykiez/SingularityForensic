using Prism.Events;
using Singularity.UI.Case.Contracts;

namespace Singularity.UI.Case.Events {
    //案件关闭事件;
    public class CloseCaseEvent : PubSubEvent {
    }
    //案件被加载事件;
    public class CaseLoadedEvent : PubSubEvent {
    }
   
    //当案件文件被加载时发生;
    public class CaseFileLoadedEvent<TCaseFile> : PubSubEvent<TCaseFile> where TCaseFile : ICaseFile {

    }

    //当新建的案件文件被附加时发生;
    public class CaseFileAddedEvent<TCaseFile> : PubSubEvent<TCaseFile> where TCaseFile : ICaseFile {

    }

    
}
