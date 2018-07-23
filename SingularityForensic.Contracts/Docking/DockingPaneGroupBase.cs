using SingularityForensic.Contracts.Docking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Docking {
    public abstract class DockingPaneGroupBase : IDockingPaneGroup {
        public abstract string ContainerGUID { get; }

        public abstract string GUID { get; }

        public virtual bool NoStyle { get; }
    }
}
