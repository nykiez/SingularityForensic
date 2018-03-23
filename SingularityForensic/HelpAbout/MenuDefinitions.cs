using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Contracts.MainMenu;
using SingularityForensic.Contracts.MainMenu;
using System.ComponentModel.Composition;

namespace SingularityForensic.HelpAbout {
    //菜单组定义;
    public static class MenuGroupDefinitions {
        [Export]
        public static readonly MenuItemGroup OptionsMenuGroup = new MenuItemGroup(MenuConstants.AboutGroup,36) {
            Text = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("OptionsShipMenuText")
        };

        [Export]
        public static readonly MenuItemGroup HelpMenuGroup = new MenuItemGroup(MenuConstants.AboutGroup, 48) {
            Text = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("HelpMenuText")
        };

    }
}
