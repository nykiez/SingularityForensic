using CDFC.Parse.Contracts;
using CDFCUIContracts.Abstracts;
using Prism.Mvvm;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.FileSystem.ViewModels {
    public class FileDetailTabModel : BindableBase,ITabModel {
        public string Header => FindResourceString("DetailInfo");

        private IFile file;
        public IFile File {
            get {
                return file;
            }
            set {
                SetProperty(ref file, value);
            }
        }

        //public event EventHandler<IFile> FileChanged;
    }
}
