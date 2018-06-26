using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using System.ComponentModel.Composition;
using System.Windows;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 分区加入文档时呈现主视图;
    /// </summary>
    /// <param name="tuple"></param>
    [Export(typeof(IDocumentAddedEventHandler))]
    class OnDocumentAddedShowPartitionBrowserHandler : IDocumentAddedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle((IDocumentBase tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            if (!(enumDoc.GetInstance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) is IDevice device)) {
                return;
            }

            var partitionBrowser = ServiceProvider.Current?.
                GetInstance<FrameworkElement>(Constants.PartitionsBrowserView);
            if (partitionBrowser == null) {
                LoggerService.WriteCallerLine($"{nameof(partitionBrowser)} can't be null.");
                return;
            }

            var vm = FileExplorerDataContextFactory.CreatePartitionsBrowserViewModel(device);
            partitionBrowser.DataContext = vm;

            enumDoc.SetInstance(vm, Contracts.FileExplorer.Constants.DocumentTag_PartitionsBrowserViewModel);
            enumDoc.MainUIObject = partitionBrowser;
        }
    }
}
