using SingularityForensic.Modules.MainMenu.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.Relevance {
    public static class MenuGroupDefinitions {
        [Export]
        public static readonly MenuItemGroup RelationShipMenuGroup = new MenuItemGroup(12) {
            Text = FindResourceString("BehaviorAnaMenuText")
        };
    }
}
