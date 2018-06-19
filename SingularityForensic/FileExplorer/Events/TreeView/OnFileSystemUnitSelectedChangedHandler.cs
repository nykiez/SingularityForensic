using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using SingularityForensic.FileExplorer.Helpers;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 加入文件系统节点响应(左键);
    /// </summary>
    [Export(typeof(ITreeUnitSelectedChangedEventHandler))]
    class OnFileSystemUnitSelectedChangedHandler : ITreeUnitSelectedChangedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle((ITreeUnit unit, ITreeService treeService) tuple) {
            if (tuple.unit == null) {
                return;
            }

            if (tuple.treeService != Contracts.MainPage.MainTreeService.Current) {
                return;
            }

            if (tuple.unit.TypeGuid != Contracts.FileExplorer.Constants.TreeUnitType_FileSystem) {
                return;
            }

            var file = tuple.unit.GetInstance<IFile>(Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
            if (file != null) {
                FileExplorerUIHelper.GetOrAddFileDocument(file);
            }
        }
    }
}
