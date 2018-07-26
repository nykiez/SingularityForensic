using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Docking;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.MainPage {
  
    //[Export(typeof(IDockingPane))]
    //class DocumentDockingPane : DockingPaneBase {
    //    public override string InitPaneGroupGUID => Contracts.MainPage.Constants.DockingPaneGroupGUID_Document;

    //    public override string RegionName => Contracts.MainPage.Constants.MainPageDocumentRegion;

    //    public override string GUID => Contracts.MainPage.Constants.DockingPaneGUID_Document;

    //    public override Visibility PaneHeaderVisibility { get; set; } = Visibility.Collapsed;
    //}

    [Export(typeof(IDockingPane))]
    class LeftDockingPane : DockingPaneBase {
        public override string InitPaneGroupGUID => Contracts.MainPage.Constants.DockingPaneGroupGUID_Left;

        public override string RegionName => Contracts.MainPage.Constants.NodeTreeRegion;

        public override string GUID => Contracts.MainPage.Constants.DockingPaneGUID_Document;

        public override string Header { get; set; } = LanguageService.FindResourceString("CaseData");

        public override double InitialWidth { get; } = 210;

        public override bool CanUserClose { get; set; } = false;
    }
    
}
