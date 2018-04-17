using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainMenu;
using System.ComponentModel.Composition;

namespace SingularityForensic.Casing {
    public static class MenuItemDefinitions {
        [Export]
        public static readonly MenuButtonItem OpenCaseMenuItem = new MenuButtonItem(
            MenuConstants.MenuMainGroup,
            LanguageService.Current?.FindResourceString("OpenCase")) {
            Command = CsUIService.OpenCaseCommand,
            IconSource = IconSources.OpenCaseIcon
        };

       

        //关闭案件菜单;
        [Export]
        public static MenuButtonItem CloseCaseMenuItem = new MenuButtonItem(
                        MenuConstants.MenuMainGroup,
                       LanguageService.Current?.FindResourceString("CloseCase")) {
            Command = CsUIService.CloseCaseCommand,
            IconSource = IconSources.CloseCaseIcon
        };
        

        [Export]
        public static readonly MenuButtonItem CreateCaseMenuItem =
            new MenuButtonItem(
                MenuConstants.MenuMainGroup,
               LanguageService.Current?.FindResourceString("CreateNewCase"), 0) {
                    Command = CsUIService.CreateCaseCommand,
                    IconSource = IconSources.CreateCaseIcon
            };

        private static CaseUIService _csUIService;
        public static CaseUIService CsUIService => _csUIService ?? (_csUIService = ServiceProvider.GetInstance<CaseUIService>());
    }
}
