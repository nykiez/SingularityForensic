using Prism.Commands;
using SingularityForensic.Contracts.FileExplorer.Models;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Models {
    public class NavNodeModel:INavNodeModel {
        public NavNodeModel(IFile file) {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            this.File = file;
        }
        public IFile File { get; private set; }

        private string _name;
        public string Name {
            get {
                if(_name == null) {
                    if(File is IPartition part) {
                        _name = FileExtensions.GetPartFixAndName(part);
                    }
                    else {
                        _name = File.Name;
                    }
                }
                return _name;
            }
        }

        //选择节点跳转的命令;
        private DelegateCommand escapeToCommand;
        public DelegateCommand EscapeToCommand {
            get {
                return escapeToCommand ??
                    (escapeToCommand = new DelegateCommand(() => {
                        EscapeRequired?.Invoke(this, this.File);
                    }));
            }
        }
        //跳转事件;
        public event EventHandler<IFile> EscapeRequired;

        ~NavNodeModel() {

        }
    }

    [Export(typeof(INavNodeModelFactory))]
    public class NavNodeModelFactoryImpl : INavNodeModelFactory {
        public INavNodeModel CreateNew(IFile file) => new NavNodeModel(file);


    }

}
