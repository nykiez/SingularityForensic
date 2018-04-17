using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.TreeView.Events {
    //当属性节点发生清除时发生;
    public class TreeUnitsClearingEvent : PubSubEvent<(CancelEventArgs args, ITreeService treeService)> {

    }
}
