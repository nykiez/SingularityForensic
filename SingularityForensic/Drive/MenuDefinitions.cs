using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Contracts.MainMenu;
using SingularityForensic.Contracts.MainMenu;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Drive {
    static class MenuDefinitions {
        private static DelegateCommand _addImgCommand;

        public static DelegateCommand AddImgCommand => _addImgCommand ??
            (_addImgCommand = new DelegateCommand(
                () => {
                    ServiceProvider.Current.GetInstance<DriveService>().AddDrive();
                }
            ));

        [Export]
        public static readonly MenuButtonItem AddDriveMenuItem = new MenuButtonItem(
            MenuConstants.MenuMainGroup,
            LanguageService.FindResourceString(Constants.AddDriveMenuText), 4) {
            Command = AddImgCommand,
            IconSource = IconSources.AddDriveIcon
        };
    }
}
