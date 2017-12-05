using Singularity.Contracts.Contracts.MainMenu;
using Singularity.Contracts.MainMenu;
using System.ComponentModel.Composition;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Modules.HelpAbout {
    //菜单组定义;
    public static class MenuGroupDefinitions {
        [Export]
        public static readonly MenuItemGroup OptionsMenuGroup = new MenuItemGroup(MenuConstants.AboutGroup,36) {
            Text = FindResourceString("OptionsShipMenuText")
        };

        [Export]
        public static readonly MenuItemGroup HelpMenuGroup = new MenuItemGroup(MenuConstants.AboutGroup, 48) {
            Text = FindResourceString("HelpMenuText")
        };

    }
}
