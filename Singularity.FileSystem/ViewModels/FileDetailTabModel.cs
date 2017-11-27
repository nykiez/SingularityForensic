using CDFC.Parse.Contracts;
using CDFCUIContracts.Abstracts;
using Prism.Mvvm;
using Singularity.UI.FileSystem.Interfaces;
using System;
using System.Text;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.FileSystem.ViewModels {
    public class FileDetailTabModel : BindableBase,ITabModel {
        public FileDetailTabModel(IFileSystemServiceProvider fsProvider) {
            if(fsProvider == null) {
                throw new ArgumentNullException(nameof(fsProvider));   
            }

            this.FSProvider = fsProvider;
        }

        public IFileSystemServiceProvider FSProvider { get; }
        public string Header => FindResourceString("DetailInfo");

        private string _text;
        public string Text {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private IFile file;
        public IFile File {
            get {
                return file;
            }
            set {
                if (value != null) {
                    var comma = FindResourceString("Comma");
                    var sb = new StringBuilder();
                    sb.AppendLine($"{FindResourceString("BasicFileInfo")}");
                    sb.AppendLine($"{FindResourceString("FileName")}{comma}{value.Name}");
                    sb.AppendLine($"{FindResourceString("FileSize")}{comma}{value.Size}{FindResourceString("Byte")}");
                    sb.AppendLine();
                    

                }
                SetProperty(ref value, value);
            }
        }

        //public event EventHandler<IFile> FileChanged;
    }
}
