using Prism.Events;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Contracts.Casing.Events {
    //案件被加载事件;
    public class CaseLoadedEvent : PubSubEvent {
    }

    public interface ICaseLoadedEventHandler : IEventHandler {

    }
}
