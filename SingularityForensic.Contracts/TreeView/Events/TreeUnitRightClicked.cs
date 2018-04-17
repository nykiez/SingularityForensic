using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.TreeView.Events {
    //节点被右击时发生;
    public class TreeUnitRightClicked : PubSubEvent<(ITreeUnit unit, ITreeService treeService)> {

    }
    
    
}
