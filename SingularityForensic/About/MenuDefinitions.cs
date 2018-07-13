using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.MainMenu;
using System.ComponentModel.Composition;

namespace SingularityForensic.About {
    //菜单组定义;
    public static class MenuGroupDefinitions {
        [Export]
        public static readonly MenuItemGroup HelpMenuGroup = new MenuItemGroup(MenuConstants.MenuGroupGUID_About, 48) {
            Text = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("HelpMenuText")
        };

    }
}
