using Prism.Events;

namespace Singularity.Contracts.Case.Events {
    //当案件文件被加载时发生;
    public class CaseFileLoadedEvent<TCaseFile> : PubSubEvent<TCaseFile> where TCaseFile : ICaseEvidence {

    }
    
}
