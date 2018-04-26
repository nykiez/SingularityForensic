using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.Contracts.ToolBar;
using SingularityForensic.Controls.ITunes.Resources;
using System.ComponentModel.Composition;

namespace SingularityForensic.ITunes {
    public static class MenuItemDefinitions {
        private static IToolBarButtonItem _addITunesBackUpToolBarItem;
        [Export]
        public static IToolBarButtonItem AddITunesBackUpToolBarItem {
            get {
                if (_addITunesBackUpToolBarItem == null) {
                    _addITunesBackUpToolBarItem = ToolBarService.CreateToolBarButtonItem(
                        new DelegateCommand(() => {
                            ServiceProvider.Current.GetInstance<ITunesBackUpService>()?.AddITunesBackUpDir();
                        }), Constants.TBButtonGUID_AddITuneBackupDir);
                    _addITunesBackUpToolBarItem.Icon = IconResources.AddITunesIcon;
                    _addITunesBackUpToolBarItem.ToolTip = LanguageService.FindResourceString(Constants.TBButtonToolTip_AddITunesBackupDir);
                    _addITunesBackUpToolBarItem.Sort = 4;
                }
                return _addITunesBackUpToolBarItem;
            }
        }

        [Export]
        public static readonly MenuButtonItem AddItunesBackUpMI = new MenuButtonItem(MenuConstants.MenuMainGroup,
            ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString(Constants.MenuItemText_AddITunesBackupDir), 5) {
            IconSource = IconResources.AddITunesIcon,
            //进行Itnues备份文件检索;
            Command = new DelegateCommand(() => {
                ServiceProvider.Current.GetInstance<ITunesBackUpService>()?.AddITunesBackUpDir();
            })
        };

        
    } 
}
