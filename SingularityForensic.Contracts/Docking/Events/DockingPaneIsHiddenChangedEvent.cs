using Prism.Events;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Docking.Events {
    public class DockingPaneIsHiddenChangedEvent:PubSubEvent<IDockingPane> {
    }

    public interface IDockingPaneIsHiddenChangedEventHandler : IEventHandler<IDockingPane> {
    }
}
