using SingularityForensic.Modules.MainMenu.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.Relevance {
    public static class MenuItemDefinitions {
        [Export]
        public static MenuItemModel TalkLogMenuItem = new MenuItemModel(MenuGroupDefinitions.RelationShipMenuGroup,FindResourceString("TalkLogMenuText"));
        
    }
}
