using CDFC.Parse.Contracts;
using CDFCUIContracts.Abstracts;
using Prism.Mvvm;
using Singularity.Contracts.FileExplorer;
using Singularity.Contracts.FileSystem;
using System;
using System.Text;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.FileExplorer.ViewModels {
    public class FileDetailTabModel : BindableBase,ITabModel {
        public FileDetailTabModel(IFileExplorerServiceProvider fsProvider) {
            if(fsProvider == null) {
                throw new ArgumentNullException(nameof(fsProvider));   
            }

            this.FSProvider = fsProvider;
        }

        public IFileExplorerServiceProvider FSProvider { get; }
        public string Header => FindResourceString("DetailInfo");

        private string _text;
        public string Text {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private IFileDetailInfoProvider _provider;
        public IFileDetailInfoProvider Provider => _provider ?? (_provider = FSProvider.FileSystemServiceProvider.GetInstance<IFileDetailInfoProvider>());

        private IFile _file;
        public IFile File {
            get {
                return _file;
            }
            set {
                if (value != null) {
                    var comma = FindResourceString("Comma");
                    var sb = new StringBuilder();
                    sb.AppendLine($"{FindResourceString("BasicFileInfo")}");
                    sb.AppendLine($"{FindResourceString("FileName")}{comma}{value.Name}");
                    sb.AppendLine($"{FindResourceString("FileSize")}{comma}{value.Size}{FindResourceString("Byte")}");
                    sb.AppendLine();
                    
                    if(Provider != null) {
                        sb.Append(Provider.GetAttachedInfo(File));
                    }
                    Text = sb.ToString();
                }
                SetProperty(ref _file, value);
            }
        }

        //public event EventHandler<IFile> FileChanged;
    }
}
