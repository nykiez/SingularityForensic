using Prism.Events;

namespace Singularity.Contracts.Case.Events {
    //当新建的案件文件被附加时发生;
    public class CaseEvidenceAddedEvent<TCaseFile> : PubSubEvent<TCaseFile> where TCaseFile : ICaseEvidence {

    }
}
