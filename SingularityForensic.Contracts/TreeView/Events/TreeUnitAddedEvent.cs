using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.TreeView.Events {
    //当树形(主分支)被附加节点时发生;
    public class TreeUnitAddedEvent : PubSubEvent<(ITreeUnit unit, ITreeService treeService)> {

    }
}
