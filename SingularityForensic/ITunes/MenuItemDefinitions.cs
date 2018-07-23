using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.MainMenu;
using System.ComponentModel.Composition;

namespace SingularityForensic.ITunes {
    public static class MenuItemDefinitions {
        [Export]
        public static readonly MenuButtonItem AddItunesBackUpMI = new MenuButtonItem(MenuConstants.MenuGroupGUID_File,
            ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString(Constants.MenuItemText_AddITunesBackupDir), 5) {
            IconSource = IconResources.AddITunesBackupIcon,
            //进行Itnues备份文件检索;
            Command = new DelegateCommand(() => {
                ServiceProvider.Current.GetInstance<ITunesBackUpService>()?.AddITunesBackUpDir();
            })
        };

        
    } 
}
