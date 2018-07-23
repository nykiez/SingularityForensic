using SingularityForensic.Contracts.Docking;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.MainPage {
    [Export(typeof(IDockingPaneContainer))]
    class LeftDockingPaneContainer : DockingContainerBase {
        public override string DockingServiceName => Contracts.MainPage.Constants.MainDockingService;

        public override string GUID => Contracts.MainPage.Constants.DockingContainerGUID_Left;

        public override DockingPosition InitDockingPosition { get; set; } = DockingPosition.Left;
    }

    [Export(typeof(IDockingPaneContainer))]
    class DocumentDockingPaneContainer : DockingContainerBase {
        public override string DockingServiceName => Contracts.MainPage.Constants.MainDockingService;

        public override string GUID => Contracts.MainPage.Constants.DockingContainerGUID_Document;

        public override DockingPosition InitDockingPosition { get; set; } = DockingPosition.Right;
    }
}
