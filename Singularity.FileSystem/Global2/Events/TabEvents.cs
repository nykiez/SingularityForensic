using CDFCUIContracts.Abstracts;
using Prism.Events;
using SingularityForensic.Modules.FileSystem.Global.TabModels;
using SingularityForensic.Modules.MainPage.Models;

namespace SingularityForensic.Modules.FileSystem.Global.Events {
    
    //内部Tab选择变更事件;
    public class InnerTabSelectedChangedEvent : PubSubEvent<ITabModel> {
    }
}
