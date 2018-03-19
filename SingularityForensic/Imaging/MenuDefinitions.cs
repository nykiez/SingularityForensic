using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Contracts.MainMenu;
using SingularityForensic.Contracts.Imaging;
using SingularityForensic.Contracts.MainMenu;
using SingularityForensic.Contracts.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Imaging {
    public static class MenuDefinitions {
        private static DelegateCommand _addImgCommand;

        public static DelegateCommand AddImgCommand => _addImgCommand ??
            (_addImgCommand = new DelegateCommand(
                () => {
                    ImgService.Current?.AddImg();
                }
            ));

        [Export]
        public static readonly MenuButtonItem AddImgMenuItem = new MenuButtonItem(
            MenuConstants.MenuMainGroup,
            ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("AddImg"), 4) {
            Command = AddImgCommand,
            IconSource = IconSources.AddImgIcon
        };


    }
}
