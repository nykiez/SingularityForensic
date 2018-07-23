using SingularityForensic.Contracts.Docking;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.MainPage {
    [Export(typeof(IDockingPaneGroup))]
    class LeftDockingPaneGroup : DockingPaneGroupBase {
        public override string ContainerGUID => Contracts.MainPage.Constants.DockingContainerGUID_Left;

        public override string GUID => Contracts.MainPage.Constants.DockingPaneGroupGUID_Left;
    }

    [Export(typeof(IDockingPaneGroup))]
    class DocumentDockingPaneGroup : DockingPaneGroupBase {
        public override string ContainerGUID => Contracts.MainPage.Constants.DockingContainerGUID_Document;

        public override string GUID => Contracts.MainPage.Constants.DockingPaneGroupGUID_Document;

        public override bool NoStyle => true;
    }
}
