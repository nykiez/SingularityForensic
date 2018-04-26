using SingularityForensic.Contracts.ToolBar;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.ToolBar.ViewModels {
    [Export]
    public class ToolBarViewModel
    {
        [ImportingConstructor]
        public ToolBarViewModel(
            [ImportMany]IEnumerable<IToolBarObjectItem> toolBarObjectItems,
            [ImportMany]IEnumerable<IToolBarButtonItem> toolBarButtonItems) {
            this._toolBarObjectItems = toolBarObjectItems;
            this._toolBarButtonItems = toolBarButtonItems;
            BuildBasicToolBars();
        }

        /// <summary>
        /// 建立基本工具栏;
        /// </summary>
        private void BuildBasicToolBars() {
            ToolBarItems.AddRange(GetAllToolBarObjectItems().OrderBy(p => p.Sort));
        }

        private IEnumerable<IToolBarObjectItem> GetAllToolBarObjectItems() {
            foreach (var btnItem in _toolBarButtonItems) {
                yield return btnItem;
            }

            foreach (var objectItem in _toolBarObjectItems) {
                yield return objectItem;
            }
        }

        public ObservableCollection<IToolBarObjectItem> ToolBarItems { get; set; } = new ObservableCollection<IToolBarObjectItem>();
        private IEnumerable<IToolBarObjectItem> _toolBarObjectItems;
        private IEnumerable<IToolBarButtonItem> _toolBarButtonItems;
    }
}
