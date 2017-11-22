using Prism.Events;
using SingularityForensic.Modules.MainPage.Models;

namespace SingularityForensic.Modules.MainPage.Global.Events {
    //选择的Tab发生变更事件;
    public class SelectedTabChangedEvent : PubSubEvent<TabModel> {
    }

    //tab被关闭的事件;
    public class TabClosedEvent : PubSubEvent<TabModel> {

    }

    public class TabAddedEvent : PubSubEvent<TabModel> {

    }
    public class TabClearedEvent : PubSubEvent {

    }
}
