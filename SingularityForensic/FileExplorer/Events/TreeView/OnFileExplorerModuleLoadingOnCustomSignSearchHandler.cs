using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.Events;
using System.ComponentModel.Composition;
using static SingularityForensic.FileExplorer.Events.TreeView.TreeServiceHelper;
namespace SingularityForensic.FileExplorer.Events.TreeView {
    /// <summary>
    /// 为设备/分区节点加入自定义签名扫描;
    /// </summary>
    [Export(typeof(IFileExplorerModuleLoadingEventHandler))]
    class OnFileExplorerModuleLoadingOnCustomSignSearchHandler : IFileExplorerModuleLoadingEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle() {
            var treeService = Contracts.MainPage.MainTreeService.Current;
            if (treeService == null) {
                return;
            }

            var comm = CommandFactory.CreateDelegateCommand(() => {
                var streamFile = GetStreamFileFromUnitSelected(treeService);
                if(streamFile == null) {
                    return;
                }

                FileExplorerStreamFileExtensions.SignSearch(streamFile);
            });

            var cmi = CommandItemFactory.CreateNew(comm,Constants.ContextCommandItemGUID_CustomSignSearch,() => GetStreamFileFromUnitSelected(treeService) != null);
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_CustomSignSearch);
            cmi.Sort = 12;

            treeService.AddContextCommand(cmi);
        }
        
    }
}
