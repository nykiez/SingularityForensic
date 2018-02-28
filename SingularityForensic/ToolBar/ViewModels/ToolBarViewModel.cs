using SingularityForensic.Contracts.Contracts.MainMenu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.ToolBar.ViewModels
{
    [Export]
    public class ToolBarViewModel
    {
        [ImportingConstructor]
        public ToolBarViewModel(
            [ImportMany]IEnumerable<MenuButtonItemModel> menuItems) {
            this._menuItems = menuItems;
            BuildBasicToolBars();
        }

        /// <summary>
        /// 建立基本工具栏;
        /// </summary>
        private void BuildBasicToolBars() {
            foreach (var item in _menuItems) {
                Tools.Add(item);
            }
            
        }
        public ObservableCollection<MenuButtonItemModel> Tools { get; set; } = new ObservableCollection<MenuButtonItemModel>();
        private IEnumerable<MenuButtonItemModel> _menuItems;
    }
}
