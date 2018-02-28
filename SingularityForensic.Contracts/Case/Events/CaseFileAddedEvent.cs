using Prism.Events;

namespace SingularityForensic.Contracts.Case.Events {
    //当新建的案件文件被附加时发生;
    public class CaseEvidenceAddedEvent:PubSubEvent<CaseEvidence> {
        
    }
}
