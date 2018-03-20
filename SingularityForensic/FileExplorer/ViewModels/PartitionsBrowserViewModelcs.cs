using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System.Collections.ObjectModel;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.FileExplorer.ViewModels {
    //磁盘-分区列表视图;
    public class PartitionsBrowserViewModel : FolderBrowserViewModel {
        public PartitionsBrowserViewModel(Device device) : base(device) {

        }

        private ObservableCollection<CommandItem> _contextCommands;
        public override ObservableCollection<CommandItem> ContextCommands {
            get {
                if (_contextCommands == null) {
                    var mainViewerCommandItem = new CommandItem {  CommandName = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("ViewerProgram") };
                    mainViewerCommandItem.Children.AddRange(ViewersCommands);
                    _contextCommands = new ObservableCollection<CommandItem> {
                        new CommandItem{ CommandName=LanguageService.FindResourceString("FileDetailInfo") , Command = ShowFileDetailCommand }
                    };
                    _contextCommands.AddRange(base.ContextCommands);

                }
                return _contextCommands;
            }
            set => _contextCommands = value;
        }
    }
}
