using CDFCUIContracts.Abstracts;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;

namespace SingularityForensic.Adb.ViewModels.AdbGrid {
    public class AdbPreviewViewModel : BindableBase, ITabModel {
        public string Header => ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("Preview");
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
