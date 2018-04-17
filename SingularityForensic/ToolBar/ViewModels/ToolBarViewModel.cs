using SingularityForensic.Contracts.MainMenu;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace SingularityForensic.ToolBar.ViewModels {
    [Export]
    public class ToolBarViewModel
    {
        [ImportingConstructor]
        public ToolBarViewModel(
            [ImportMany]IEnumerable<MenuButtonItem> menuItems) {
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
        public ObservableCollection<MenuButtonItem> Tools { get; set; } = new ObservableCollection<MenuButtonItem>();
        private IEnumerable<MenuButtonItem> _menuItems;
    }
}
