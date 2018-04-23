using SingularityForensic.Contracts.App;
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
    /// 分区加入文档时的呈现主视图;
    /// </summary>
    /// <param name="tuple"></param>
    [Export(typeof(IDocumentAddedEventHandler))]
    public class OnDocumentAddedShowFolderBrowserEventHandler : 
        EventHandlerBase<(IDocumentBase tab, IDocumentService owner)>,IDocumentAddedEventHandler {
        public override void Handle((IDocumentBase tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }

            var part = enumDoc.GetIntance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) as IPartition;
            if (part == null) {
                return;
            }

            var vm = FileExplorerViewModelFactory.CreateFolderBrowserViewModel(part);

            var folderBrowser = ViewProvider.GetView(Constants.FolderBrowserView);
            if (folderBrowser is FrameworkElement elem) {
                elem.DataContext = vm;
            }

            //设定文件资源管理器模型关联实体;
            enumDoc.SetInstance(vm, Contracts.FileExplorer.Constants.DocumentTag_FolderBrowserViewModel);
            enumDoc.MainUIObject = folderBrowser;
        }
    }
}
