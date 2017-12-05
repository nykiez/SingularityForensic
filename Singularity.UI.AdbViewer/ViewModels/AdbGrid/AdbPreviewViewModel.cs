using CDFCUIContracts.Abstracts;
using Prism.Mvvm;
using Singularity.Contracts.FileExplorer;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.AdbViewer.ViewModels.AdbGrid {
    public class AdbPreviewViewModel : BindableBase, ITabModel {
        public string Header => FindResourceString("Preview");
        private IPreviewer previewer;
        public IPreviewer Previewer {
            get {
                return previewer;
            }
            set {
                SetProperty(ref previewer, value);
            }
        }
    }
    
}
