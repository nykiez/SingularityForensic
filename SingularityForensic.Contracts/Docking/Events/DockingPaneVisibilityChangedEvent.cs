using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Docking.Events {
    /// <summary>
    /// DockingPane的头部栏可见状态发生变化时发生;
    /// </summary>
    public class DockingPaneHeaderVisibilityChangedEvent:PubSubEvent<IDockingPane> {
    }

    public interface IDockingPaneHeaderVisibilityChangedEventHandler : IEventHandler<IDockingPane> {

    }
}
