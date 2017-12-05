using Singularity.Contracts.Contracts.MainMenu;
using Singularity.Contracts.MainMenu;
using System.ComponentModel.Composition;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Modules.MainPage {
    public static class MenuGroupDefinitions {
        [Export]
        public static readonly MenuItemGroup MainPageMenuGroup = new MenuItemGroupEx(MenuConstants.MenuMainGroup,0) {
            Text = FindResourceString("MainPageMenuText"),
            IconSource = Resources.IconSources.MenuHomeIcon
        };
    }
}
