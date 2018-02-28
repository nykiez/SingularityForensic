using CDFC.Parse.Abstracts;
using CDFCUIContracts.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System.Collections.ObjectModel;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Controls.FileExplorer.ViewModels {
    //磁盘-分区列表视图;
    public class PartitionsBrowserViewModel : FolderBrowserViewModel {
        public PartitionsBrowserViewModel(Device device) : base(device) {

        }

        private ObservableCollection<ICommandItem> _contextCommands;
        public override ObservableCollection<ICommandItem> ContextCommands {
            get {
                if (_contextCommands == null) {
                    var mainViewerCommandItem = new CommandItem { Children = ViewersCommands, CommandName = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("ViewerProgram") };
                    _contextCommands = new ObservableCollection<ICommandItem> {
                        new CommandItem{ CommandName=FindResourceString("FileDetailInfo") , Command = ShowFileDetailCommand }
                    };
                    _contextCommands.AddRange(base.ContextCommands);

                }
                return _contextCommands;
            }
            set => _contextCommands = value;
        }
    }
}
