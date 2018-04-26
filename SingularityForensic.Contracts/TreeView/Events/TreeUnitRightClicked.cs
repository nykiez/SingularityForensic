using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.TreeView.Events {
    /// <summary>
    /// 节点被右击时发生;
    /// </summary>
    public class TreeUnitRightClicked : PubSubEvent<(ITreeUnit unit, ITreeService treeService)> {

    }

    /// <summary>
    /// 右击事件处理器;
    /// </summary>
    public interface ITreeUnitRightClickedEventHandler:IEventHandler<(ITreeUnit unit,ITreeService treeService)> {

    }


}
