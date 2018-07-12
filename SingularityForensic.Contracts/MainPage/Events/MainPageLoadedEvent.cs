using Prism.Events;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Contracts.MainPage.Events {
    /// <summary>
    /// 主页被加载事件;
    /// </summary>
    public class MainPageLoadedEvent:PubSubEvent {
    }

    public interface IMainPageLoadedEventHandler : IEventHandler {

    }
}
