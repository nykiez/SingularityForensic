using Prism.Events;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Contracts.Casing.Events {
    /// <summary>
    /// 当案件文件已被加载时发生的事件;
    /// </summary>
    public class CaseEvidenceLoadedEvent : PubSubEvent<ICaseEvidence> {

    }

    public interface ICaseEvidenceLoadedEventHanlder : IEventHandler<ICaseEvidence> {

    }
}
