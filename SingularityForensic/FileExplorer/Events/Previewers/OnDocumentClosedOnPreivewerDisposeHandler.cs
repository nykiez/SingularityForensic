using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Previewers;
using System;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 文档关闭时释放预览器;
    /// </summary>
    [Export(typeof(IDocumentClosedEventHandler))]
    class OnDocumentClosedOnPreivewerDisposeHandler : IDocumentClosedEventHandler {
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle((IDocumentBase doc, IDocumentService owner) tuple) {
            //释放预览器;
            var previewerDoc = tuple.doc?.GetIntance<IDocument>(Constants.Document_FilePreviewer);
            if (previewerDoc == null) {
                return;
            }

            try {
                previewerDoc.GetIntance<IPreviewer>(Constants.DocumentTag_FilePreviewer)?.Dispose();
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }
    }
}
