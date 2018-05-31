using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.StatusBar;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SingularityForensic.StatusBar {
    [Export(typeof(IStatusBarService))]
    class StatusBarServiceImpl : IStatusBarService {
        [ImportingConstructor]
        public StatusBarServiceImpl(IStatusBar statusBar) {
            _stackGrid = StackGridFactory.CreateNew<IStatusBarObjectItem>(statusBar.Grid);
            _stackGrid.Orientation = Orientation.Horizontal;
        }

        public void Initialize() {
            _items.Clear();
            _stackGrid.Clear();

            //添加默认状态栏项;
            var defaultItem = CreateStatusBarTextItem(Constants.StatusBarItemGUID_Default);
            defaultItem.Margin = new Thickness(3, 0, 3, 0);
            AddStatusBarItem(defaultItem,new GridChildLength(new GridLength(1, GridUnitType.Star)));
        }

        private IStatusBar _statusBar;
        private IStackGrid<IStatusBarObjectItem> _stackGrid;

        private List<IStatusBarObjectItem> _items = new List<IStatusBarObjectItem>();
        public IEnumerable<IStatusBarObjectItem> Children => _items.Select(p => p);

        public void AddStatusBarItem(IStatusBarObjectItem item, GridChildLength gridChildLength, int index = -1) {
            if(item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            _items.Add(item);
            try {
                _stackGrid.AddChild<IStatusBarObjectItem>(item, gridChildLength, index);
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }

        public void Report(string text, string statusBarItemGUID = null) {
            if(statusBarItemGUID == null) {
                statusBarItemGUID = Constants.StatusBarItemGUID_Default;
            }

            var item = _items.FirstOrDefault(p => p.GUID == statusBarItemGUID);
            if(item is IStatusBarTextItem textItem) {
                textItem.Text = text;
            }
        }

        public void RemoveStatusBarItem(IStatusBarObjectItem item) {
            if(item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            if (!Children.Contains(item)) {
                LoggerService.WriteCallerLine($"{nameof(Children)} doesn't contain the {nameof(item)}");
                return;
            }

            try {
                _items.Remove(item);
                _stackGrid.Remove(item);
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }

        public IStatusBarObjectItem CreateStatusBarObjectItem(object content, string guid) => new StatusBarObjectItem(guid, content);

        public IStatusBarTextItem CreateStatusBarTextItem(string guid) => new StatusBarTextItem(guid);

        public IStatusBarTextItem GetOrCreateStatusBarTextItem(string guid, GridChildLength gridChildLength, int sort) {
            var item = Children.FirstOrDefault(p => p.GUID == guid) as IStatusBarTextItem;
            if (item == null) {
                item = CreateStatusBarTextItem(guid);
                StatusBarService.Current.AddStatusBarItem(item, gridChildLength, sort);
                item.Margin = new Thickness(12, 0, 12, 0);
            }

            return item;
        }
    }
}
