using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.FileSystem;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 分区加入文档时呈现预览;
    /// </summary>
    [Export(typeof(IDocumentAddedEventHandler))]
    class OnDocumentAddedOnPartPreviewHandler : IDocumentAddedEventHandler {
        public int Sort => 4;

        public bool IsEnabled => true;

        public void Handle((IDocumentBase tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            var haveFileCollection = enumDoc.GetInstance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) as IHaveFileCollection;
            if (haveFileCollection == null) {
                LoggerService.WriteCallerLine($"{nameof(haveFileCollection)} can't be null.");
                return;
            }

            if(haveFileCollection is IDevice) {
                return;
            }

            var doc = enumDoc.CreateNewDocument();
            doc.Title = LanguageService.FindResourceString(Constants.DocumentTitle_FilePreview);
            enumDoc.AddDocument(doc);
            enumDoc.SetInstance(doc, Constants.Document_FilePreviewer);
        }
    }
}
