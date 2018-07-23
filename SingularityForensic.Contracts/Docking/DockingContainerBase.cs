using SingularityForensic.Contracts.Docking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Docking {
    public abstract class DockingContainerBase : IDockingPaneContainer {
        public virtual DockingPosition InitDockingPosition { get ; set ; }
        public abstract string GUID { get; }

        public abstract string DockingServiceName { get; }

      
    }
}
