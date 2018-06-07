using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using SingularityForensic.FileExplorer.Helpers;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 添加子文件递归浏览的命令;
    /// </summary>
    [Export(typeof(ITreeUnitAddedEventHandler))]
    public class OnFileUnitAddedRecurBroswingHandler : EventHandlerBase<(ITreeUnit unit, ITreeService treeService)>, ITreeUnitAddedEventHandler {
        public override void Handle((ITreeUnit unit, ITreeService treeService) tuple) {
            if (tuple.unit == null) {
                return;
            }

            if (tuple.treeService != Contracts.MainPage.MainTreeService.Current) {
                return;
            }

            if (tuple.unit.TypeGuid == Contracts.FileExplorer.Constants.TreeUnitType_InnerFile) {
                var comm = new DelegateCommand(() => HandleOnInnerFileUnit(tuple.unit));
                var cmi = CommandItemFactory.CreateNew(comm);
                cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_RecurBrowse);
                tuple.unit.AddContextCommand(cmi);
            }

            if (tuple.unit.TypeGuid == Contracts.FileExplorer.Constants.TreeUnitType_FileSystem) {
                var comm = new DelegateCommand(() => HandleOnFileSystemUnit(tuple.unit));
                var cmi = CommandItemFactory.CreateNew(comm);
                cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_RecurBrowse);
                tuple.unit.AddContextCommand(cmi);
            } 
        }

        public void HandleOnInnerFileUnit(ITreeUnit unit) {
            var innerFile = unit.GetIntance<IFile>(Contracts.FileExplorer.Constants.TreeUnitTag_InnerFile);
            if (innerFile == null) {
                LoggerService.WriteCallerLine($"{nameof(innerFile)} can't be null.");
                return;
            }

            if (!(innerFile is IHaveFileCollection haveFileCollection)) {
                LoggerService.WriteCallerLine($"{nameof(haveFileCollection)} can't be null.");
                return;
            }

            HandleOnFileCollection(haveFileCollection);
        }

        public void HandleOnFileSystemUnit(ITreeUnit unit) {
            var file = unit?.GetIntance<IFile>(Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
            if(!(file is IHaveFileCollection haveFileCollection)) {
                return;
            }

            HandleOnFileCollection(haveFileCollection);
        }

        private void HandleOnFileCollection(IHaveFileCollection haveFileCollection) {
            IStreamFile streamFile = null;
            if (haveFileCollection is IStreamFile) {
                streamFile = haveFileCollection as IStreamFile;
            }
            else {
                streamFile = haveFileCollection.GetParent<IStreamFile>();
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

            folderBrowseViewModel.FillRows(haveFileCollection.GetInnerFiles());
        }
    }

    
}
