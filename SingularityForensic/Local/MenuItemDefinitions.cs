using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.MainMenu;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Local {
    public static class MenuItemDefinitions {
        [Export]
        public static readonly MenuButtonItem AddItunesBackUpMI = new MenuButtonItem(MenuConstants.MenuGroupGUID_File,
            LanguageService.FindResourceString(Constants.MenuItemText_AddLocalDir), 5) {
            IconSource = IconResources.AddLocalDirIcon,
            //进行Itnues备份文件检索;
            Command = new DelegateCommand(() => {
                ServiceProvider.Current.GetInstance<LocalFileService>()?.AddLocalDir();
            })
        };
    }
}
