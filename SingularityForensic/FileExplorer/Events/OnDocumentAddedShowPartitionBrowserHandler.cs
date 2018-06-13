using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var device = enumDoc.GetIntance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) as IDevice;
            if (device == null) {
                return;
            }

            var partitionBrowser = ServiceProvider.Current?.
                GetInstance<FrameworkElement>(Constants.PartitionsBrowserView);
            if (partitionBrowser == null) {
                LoggerService.WriteCallerLine($"{nameof(partitionBrowser)} can't be null.");
                return;
            }

            var vm = FileExplorerViewModelFactory.CreatePartitionsBrowserViewModel(device);
            partitionBrowser.DataContext = vm;

            enumDoc.SetInstance(vm, Contracts.FileExplorer.Constants.DocumentTag_PartitionsBrowserViewModel);
            enumDoc.MainUIObject = partitionBrowser;
        }
    }
}
