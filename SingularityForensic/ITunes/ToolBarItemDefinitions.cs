using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.ToolBar;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.ITunes {
    public static class ToolBarItemDefinitions {
        /// <summary>
        /// 添加ITunes备份文件夹工具栏项;;
        /// </summary>
        private static IToolBarButtonItem _addITunesBackUpToolBarItem;
        [Export]
        public static IToolBarButtonItem AddITunesBackUpToolBarItem {
            get {
                if (_addITunesBackUpToolBarItem == null) {
                    _addITunesBackUpToolBarItem = ToolBarItemFactory.CreateToolBarButtonItem(
                        new DelegateCommand(() => {
                            ServiceProvider.Current.GetInstance<ITunesBackUpService>()?.AddITunesBackUpDir();
                        }), Constants.TBButtonGUID_AddITuneBackupDir);
                    _addITunesBackUpToolBarItem.Icon = IconResources.AddITunesBackupIcon;
                    _addITunesBackUpToolBarItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_AddITunesBackupDir);
                    _addITunesBackUpToolBarItem.Sort = 4;
                }
                return _addITunesBackUpToolBarItem;
            }
        }
    }
}
