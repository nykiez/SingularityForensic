using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.StatusBar;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.FileExplorer.Events {
    /// <summary>
    /// 当当前文件行发生变更时,通知状态栏变化;
    /// </summary>
    [Export(typeof(IFolderBrowserViewModelCreatedEventHandler))]
    class OnFolderBrowserViewModelCreatedOnFilesChangedToStatusBarHandler : IFolderBrowserViewModelCreatedEventHandler {
        public int Sort => 7;

        public bool IsEnabled => true;

        public void Handle(IFolderBrowserViewModel args) {
            if(args == null) {
                return;
            }

            args.FilesChanged += (sender, e) => RefreshFilesCount(args);
        }
        
        private void RefreshFilesCount(IFolderBrowserViewModel vm) {
            long fileCount = 0;
            long regFileCount = 0;
            long dirCount = 0;

            try {
                foreach (var file in vm.Files) {
                    if(file.File is IRegularFile) {
                        regFileCount++;
                    }
                    else if(file.File is IDirectory) {
                        dirCount++;
                    }
                    fileCount++;
                }

                var fileCountItem = StatusBarService.GetOrCreateStatusBarTextItem(Constants.StatusBarItemText_FileCount, GridChildLength.Auto,5);
                var regFileCountItem = StatusBarService.GetOrCreateStatusBarTextItem(Constants.StatusBarItemGUID_RegFileCount, GridChildLength.Auto, 6);
                var dirCountItem = StatusBarService.GetOrCreateStatusBarTextItem(Constants.StatusBarItemGUID_DirectoryCount, GridChildLength.Auto, 7);

                fileCountItem.Text = $"{LanguageService.FindResourceString(Constants.StatusBarItemText_FileCount)} {fileCount}";
                regFileCountItem.Text = $"{LanguageService.FindResourceString(Constants.StatusBarItemText_RegFileCount)} {regFileCount}";
                dirCountItem.Text = $"{LanguageService.FindResourceString(Constants.StatusBarItemText_DirectoryCount)} {dirCount}";
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }
    }
}
