using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hex;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 分区选中行发生变更时,更新十六进制变化;
    /// </summary>
    [Export(typeof(IFocusedFileRowChangedEventHandler))]
    class OnFocusedFileChangedOnPartHexHandler : IFocusedFileRowChangedEventHandler {
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle((IFolderBrowserViewModel owner, IFileRow file) tuple) {
            var folderBrowserVM = tuple.owner;
            if(folderBrowserVM == null) {
                return;
            }

            var tab = DocumentService.MainDocumentService.CurrentDocuments.
                FirstOrDefault(p => p.GetInstance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) == folderBrowserVM.HaveFileCollection);
            if (tab == null) {
                return;
            }

            var partHexDataContext = tab.GetInstance<IHexDataContext>(Contracts.FileExplorer.Constants.HexDataContext_FolderBrowser_Partition);
            var fileHexDataContext = tab.GetInstance<IHexDataContext>(Contracts.FileExplorer.Constants.HexDataContext_FolderBrowser_File);

            if (!(tuple.file.File is IBlockGroupedFile blockGrouped)) {
                return;
            }

            var startLBA = blockGrouped.GetStartLBA();
            if (startLBA != null && partHexDataContext != null) {
                partHexDataContext.Position = startLBA.Value;
            }

            var fileBaseStream = blockGrouped.GetInputStream();
            if (fileHexDataContext != null) {
                fileHexDataContext.Stream?.Dispose();
                fileHexDataContext.Stream = fileBaseStream;
            }
        }
    }
}
