using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.Helpers;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 分区加入文档时呈现分区的十六进制;
    /// </summary>
    /// <param name="obj"></param>
    [Export(typeof(IDocumentAddedEventHandler))]
    class OnDocumentAddedOnPartHexHandler : IDocumentAddedEventHandler {
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle((IDocumentBase tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            if (!(enumDoc.GetInstance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) is IPartition part)) {
                return;
            }

            var hexPartTuple = FileExplorerUIHelper.GetStreamHexDocument(part);
            if (hexPartTuple == null) {
                return;
            }

            hexPartTuple.Value.doc.Title = LanguageService.FindResourceString(Constants.DocumentTitle_HexPartition);
            enumDoc.AddDocument(hexPartTuple.Value.doc);
            enumDoc.SetInstance(hexPartTuple.Value.hexDataContext, Contracts.FileExplorer.Constants.HexDataContext_FolderBrowser_Partition);
            enumDoc.SelectedDocument = hexPartTuple.Value.doc;
        }
    }
}
