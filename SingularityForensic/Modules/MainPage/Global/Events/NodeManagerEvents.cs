using CDFCUIContracts.Models;
using Prism.Events;
using System.ComponentModel;

namespace SingularityForensic.Modules.MainPage.Global.Events {
    //节点被右击时发生;
    public class TreeNodeRightClicked : PubSubEvent<ITreeUnit> {

    }

    //当选择的树形节点发生了变化时发生;
    public class SelectedNodeChangedEvent : PubSubEvent<ITreeUnit> {

    }

    //当树形任意节点被左击时发生;
    public class TreeNodeClickEvent : PubSubEvent<ITreeUnit> {

    }

    //当树形(主分支)被附加节点时发生;
    public class TreeNodeAdded : PubSubEvent<ITreeUnit> {

    }

    //当节点(主分支)被附加节点时发生;
    public class TreeNodeAdded<TNewUnit> : PubSubEvent<TNewUnit> where TNewUnit : ITreeUnit {

    }

    //当节点被附加节点时发生;
    public class TreeNodeAddedWith:PubSubEvent<(ITreeUnit,ITreeUnit)> {

    }

    //当节点被附加节点时发生;
    public class TreeNodeAddedWith<TParentUnit, TNewUnit> : PubSubEvent<(TParentUnit, TNewUnit)> where TParentUnit:ITreeUnit where TNewUnit : ITreeUnit {

    }

    

    //当属性节点发生清除时发生;
    public class TreeNodesClearingEvent : PubSubEvent<CancelEventArgs> {

    }
    
}
