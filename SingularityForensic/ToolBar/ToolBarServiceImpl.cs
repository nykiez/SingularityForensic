using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.ToolBar;
using SingularityForensic.ToolBar.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.ToolBar
{
    [Export(typeof(IToolBarService))]
    class ToolBarServiceImpl : IToolBarService {
        [ImportingConstructor]
        public ToolBarServiceImpl(ToolBarViewModel vm,
            [ImportMany]IEnumerable<IToolBarObjectItem> toolBarObjectItems,
            [ImportMany]IEnumerable<IToolBarButtonItem> toolBarButtonItems) {
            this._vm = vm;
            this._toolBarObjectItems = toolBarObjectItems;
            this._toolBarButtonItems = toolBarButtonItems;
            
        }


        private ToolBarViewModel _vm;
        private IEnumerable<IToolBarObjectItem> _toolBarObjectItems;
        private IEnumerable<IToolBarButtonItem> _toolBarButtonItems;
        private IEnumerable<IToolBarObjectItem> GetAllToolBarObjectItems() {
            foreach (var btnItem in _toolBarButtonItems) {
                yield return btnItem;
            }

            foreach (var objectItem in _toolBarObjectItems) {
                yield return objectItem;
            }
        }
        
        public IEnumerable<IToolBarObjectItem> ToolBarItems => _vm.ToolBarItems.Select(p => p);

        public void AddToolBarItem(IToolBarObjectItem item) {
            if(item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            _vm.ToolBarItems.AddOrderBy(item, p => p.Sort);
        }

        public void RemoveToolBar(IToolBarObjectItem item) {
            if(item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            if (_vm.ToolBarItems.Contains(item)) {
                return;
            }

            _vm.ToolBarItems.Remove(item);
        }

        public void Initialize() {
            _vm.ToolBarItems.Clear();
            _vm.ToolBarItems.AddRange(GetAllToolBarObjectItems().OrderBy(p => p.Sort));
        }
    }
}
