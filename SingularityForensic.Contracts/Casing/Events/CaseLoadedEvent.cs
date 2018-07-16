using Prism.Events;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Contracts.Casing.Events {
    /// <summary>
    /// 案件已经被加载事件;
    /// </summary>
    public class CaseLoadedEvent : PubSubEvent {
    }

    public interface ICaseLoadedEventHandler : IEventHandler {

    }
}
