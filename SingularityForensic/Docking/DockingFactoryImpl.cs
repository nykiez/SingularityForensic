using SingularityForensic.Contracts.Docking;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Docking {
    [Export(typeof(IDockingFactory))]
    class DockingFactoryImpl : IDockingFactory {
        public IDockingPane CreatePane(string guid, string initGroupGUID, string regionName) => new DockingPane(guid, initGroupGUID, regionName);

        public IDockingPaneContainer CreatePaneContainer(string guid) => new DockingContainerBase(guid);

        public IDockingPaneGroup CreatePaneGroup(string guid, string containerGUID) => new DockingPaneGroup(guid, containerGUID);
    }
}
