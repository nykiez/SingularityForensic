using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.Contracts.ToolBar;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingularityForensic.FileExplorer {
    /// <summary>
    /// 加载名称类别工具栏;
    /// </summary>
    
    static class LoadCategoryNameDescriptorsMenuItemDefinitions {
        private static IToolBarButtonItem _loadCategoryNameDescriptorsToolBarItem;
        [Export]
        public static IToolBarButtonItem LoadCategoryNameDescriptorsToolBarItem {
            get {
                if(_loadCategoryNameDescriptorsToolBarItem == null) {
                    _loadCategoryNameDescriptorsToolBarItem = ToolBarItemFactory.CreateToolBarButtonItem(
                        CommandFactory.CreateDelegateCommand(LoadCategoryNameDescriptors),
                        Constants.TBButtonGUID_LoadCategoryNameFile
                    );
                    _loadCategoryNameDescriptorsToolBarItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_LoadCategoryNameFile);
                    _loadCategoryNameDescriptorsToolBarItem.Icon = IconResources.LoadCategoryNameDescriptorsIcon;
                    _loadCategoryNameDescriptorsToolBarItem.Sort = 64;
                }
                return _loadCategoryNameDescriptorsToolBarItem;

            }
        }
        
        private static void LoadCategoryNameDescriptors() {
            var fileName = DialogService.Current.OpenFile();
            if (string.IsNullOrEmpty(fileName)) {
                return;
            }

            try {
                CategoryNameService.LoadDescriptorsFromFile(fileName);
                var dts = SingularityForensic.Contracts.FileExplorer.FileExplorerDataContextFactory.Current?.GetAllFolderBrowserDataContext();
                if(dts == null) {
                    return;
                }

                ThreadInvoker.BackInvoke(() => {
                    foreach (var dt in dts) {
                        var files = dt.FolderBrowserViewModel?.Files;
                        if (files == null) {
                            continue;
                        }
                        foreach (var file in files) {
                            file.NotifyProperty(Constants.FileMetaDataGUID_NameCategory);
                        }
                    }
                });
                
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
                MsgBoxService.Current.Show(ex.Message);
            }
        }
        
    }
}
