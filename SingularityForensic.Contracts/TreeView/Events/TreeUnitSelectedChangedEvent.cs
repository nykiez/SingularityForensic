using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.TreeView.Events {
    //当选择的树形节点发生了变化时发生;
    public class TreeUnitSelectedChangedEvent : PubSubEvent<(ITreeUnit unit,ITreeService treeService)> {

    }

    public interface ITreeUnitSelectedChangedEventHandler:IEventHandler<(ITreeUnit unit, ITreeService treeService)> {

    }
}
