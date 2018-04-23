using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 点击节点子文件(分区 / 目录)时,操作文档区域;
    /// </summary>
    /// <param name="tuple"></param>
    [Export(typeof(ITreeUnitSelectedChangedEventHandler))]
    public class OnInnerFileUnitSelectedEventHandler :
        EventHandlerBase<(ITreeUnit unit, ITreeService treeService)>,
        ITreeUnitSelectedChangedEventHandler {

        public override void Handle((ITreeUnit unit, ITreeService treeService) tuple) {
            if (tuple.unit == null) {
                return;
            }

            if (tuple.treeService != Contracts.MainPage.MainTreeService.Current) {
                return;
            }

            if (tuple.unit.TypeGuid != Contracts.FileExplorer.Constants.TreeUnitType_InnerFile) {
                return;
            }

            var innerFile = tuple.unit.GetIntance<IFile>(Contracts.FileExplorer.Constants.TreeUnitTag_InnerFile);
            if (innerFile == null) {
                LoggerService.WriteCallerLine($"{nameof(innerFile)} can't be null.");
                return;
            }

            if (!(innerFile is IHaveFileCollection haveFileCollection)) {
                LoggerService.WriteCallerLine($"{nameof(haveFileCollection)} can't be null.");
                return;
            }

            IStreamFile streamFile = null;
            if (innerFile is IStreamFile) {
                streamFile = innerFile as IStreamFile;
            }
            else {
                streamFile = innerFile.GetParent<IStreamFile>();
            }

            if (streamFile == null) {
                LoggerService.WriteCallerLine($"{nameof(streamFile)} can't be null.");
                return;
            }

            var doc = FileExplorerUIHelper.GetOrAddFileDocument(streamFile);
            if (doc == null) {
                LoggerService.WriteCallerLine($"{nameof(doc)} can't be null.");
                return;
            }

            var folderBrowseViewModel = doc.GetIntance<IFolderBrowserViewModel>(Contracts.FileExplorer.Constants.DocumentTag_FolderBrowserViewModel);
            if (folderBrowseViewModel == null) {
                LoggerService.WriteCallerLine($"{nameof(folderBrowseViewModel)} can't be null.");
                return;
            }

            folderBrowseViewModel.FillWithCollection(haveFileCollection);
        }
    }
}
