using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 分区加入文档时的呈现主视图;
    /// </summary>
    /// <param name="tuple"></param>
    [Export(typeof(IDocumentAddedEventHandler))]
    public class OnDocumentAddedShowFolderBrowserHandler : IDocumentAddedEventHandler {
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle((IDocumentBase tab, IDocumentService owner) tuple) {
            if (!(tuple.tab is IEnumerableDocument enumDoc)) {
                return;
            }
            

            var haveFileCollection = enumDoc.GetIntance<IFile>(Contracts.FileExplorer.Constants.DocumentTag_File) as IHaveFileCollection;
            if (haveFileCollection == null) {
                return;
            }

            //若是设备,因设备有专门的视图,中止操作;
            if (haveFileCollection is IDevice) {
                return;
            }

            var vm = FileExplorerViewModelFactory.CreateFolderBrowserViewModel(haveFileCollection);

            var folderBrowser = ViewProvider.CreateView(Constants.FolderBrowserView,vm);

            //设定文件资源管理器模型关联实体;
            enumDoc.SetInstance(vm, Contracts.FileExplorer.Constants.DocumentTag_FolderBrowserViewModel);
            enumDoc.MainUIObject = folderBrowser;
        }
    }
}
