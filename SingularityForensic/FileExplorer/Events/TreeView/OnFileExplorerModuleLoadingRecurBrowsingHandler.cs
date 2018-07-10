using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.FileExplorer.Helpers;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events.TreeView {
    /// <summary>
    /// 添加子文件递归浏览的命令;
    /// </summary>
    [Export(typeof(IFileExplorerModuleLoadingEventHandler))]
    public class OnFileExplorerModuleLoadingRecurBrowsingHandler :  IFileExplorerModuleLoadingEventHandler {
        public int Sort => 4;

        public bool IsEnabled => true;

        public void Handle() {
            var treeService = Contracts.MainPage.MainTreeService.Current;
            if (treeService == null) {
                return;
            }
            
            var comm = CommandFactory.CreateDelegateCommand(() =>{
                if (CheckInnerFileUnitSelected(treeService) ){
                    HandleOnInnerFileUnit(treeService.SelectedUnit);
                }
                else if(CheckFileSystemUnitSelected(treeService)){
                    HandleOnFileSystemUnit(treeService.SelectedUnit);
                }
            });
            var cmi = CommandItemFactory.CreateNew(comm,Constants.ContextCommandItemGUID_RecurBrowse,() => CheckFileSystemUnitSelected(treeService) || CheckInnerFileUnitSelected(treeService));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_RecurBrowse);
            treeService.AddContextCommand(cmi);
        }

        private void HandleOnInnerFileUnit(ITreeUnit unit) {
            var innerFile = unit.GetInstance<IFile>(Contracts.FileExplorer.Constants.TreeUnitTag_InnerFile);
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

        private void HandleOnFileSystemUnit(ITreeUnit unit) {
            var file = unit?.GetInstance<IFile>(Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
            if(!(file is IHaveFileCollection haveFileCollection)) {
                return;
            }

            HandleOnFileCollection(haveFileCollection);
        }

        private static bool CheckInnerFileUnitSelected(ITreeService treeService) => treeService.CheckTypedUnitSelected(Contracts.FileExplorer.Constants.TreeUnitType_InnerFile);

        private static bool CheckFileSystemUnitSelected(ITreeService treeService) => treeService.CheckTypedUnitSelected(Contracts.FileExplorer.Constants.TreeUnitType_FileSystem);

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

            var folderBrowseDataContext = doc.GetInstance<IFolderBrowserDataContext>(Contracts.FileExplorer.Constants.DocumentTag_FolderBrowserDataContext);
            if (folderBrowseDataContext == null) {
                LoggerService.WriteCallerLine($"{nameof(folderBrowseDataContext)} can't be null.");
                return;
            }
            var folderBrowseViewModel = folderBrowseDataContext.FolderBrowserViewModel;
            if(folderBrowseViewModel == null) {
                LoggerService.WriteCallerLine($"{nameof(folderBrowseViewModel)} can't be null.");
                return;
            }

            folderBrowseViewModel.FillRows(haveFileCollection.GetInnerFiles());
        }
    }

    
}
