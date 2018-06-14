using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.Helpers;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// /// <summary>
    /// 分区加入文档时呈现文件的十六进制;
    /// </summary>
    /// <param name="obj"></param>
    /// </summary>
    [Export(typeof(IDocumentAddedEventHandler))]
    class OnDocumentAddedOnFileHexHandler : IDocumentAddedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle((IDocumentBase tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            var haveFileCollection = enumDoc.GetInstance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) as IHaveFileCollection;
            if (haveFileCollection == null) {
                return;
            }

            if(haveFileCollection is IDevice) {
                return;
            }

            var hexFileTuple = FileExplorerUIHelper.GetStreamHexDocument(null);
            if (hexFileTuple == null) {
                return;
            }
            hexFileTuple.Value.doc.Title = LanguageService.FindResourceString(Constants.DocumentTitle_HexFile);

            enumDoc.AddDocument(hexFileTuple.Value.doc);
            enumDoc.SetInstance(hexFileTuple.Value.hexDataContext, Contracts.FileExplorer.Constants.HexDataContext_FolderBrowser_File);
        }
    }
}
