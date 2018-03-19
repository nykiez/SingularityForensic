using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Document {
    [Export(typeof(IDocumentTabService))]
    public class DocumentTabService : IDocumentTabService {
        //增加Tab;
        public void AddTab(IDocumentTab tab) {
            if(tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }

            if (_tabs.Contains(tab)) {
                SelectedTab = tab;
                return;
            }

            TabAdding?.Invoke(this, tab);
            _tabs.Add(tab);
            TabAdded?.Invoke(this, tab);
        }

        private List<IDocumentTab> _tabs = new List<IDocumentTab>();

        //所有的Tab;
        public IEnumerable<IDocumentTab> CurrentTabs => _tabs.Select(p => p);

        //关闭所有Tab;
        public void CloseAllTabs() {
            var cEvg = new CancelEventArgs();
            TabsClearing?.Invoke(this,cEvg);
            if (cEvg.Cancel) {
                return;
            }

            foreach (var tab in _tabs) {
                TabClosed?.Invoke(this, tab);
            }
        }

        //关闭Tab
        public void CloseTab(IDocumentTab tab) {
            if(tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }

            if (!_tabs.Contains(tab)) {
                throw new Exception($"{nameof(tab)} is not inside the list.");
            }
            
            var cEvg = new CancelEventArgs();
            this.TabClosing?.Invoke(this, (tab,cEvg));
            if (cEvg.Cancel) {
                return;
            }

            _tabs.Remove(tab);

            this.TabClosed?.Invoke(this, tab);
        }

        private IDocumentTab _selectedTab;

        public event EventHandler<IDocumentTab> TabAdding;
        public event EventHandler<IDocumentTab> TabAdded;
        public event EventHandler<CancelEventArgs> TabsClearing;
        public event EventHandler TabsCleared;
        public event EventHandler<(IDocumentTab tab, CancelEventArgs e)> TabClosing;
        public event EventHandler<IDocumentTab> TabClosed;
        public event EventHandler<IDocumentTab> SelectedTabChanged;

        public IDocumentTab SelectedTab {
            get => _selectedTab;
            set {
                if(value == null) {
                    throw new ArgumentNullException(nameof(SelectedTab));
                }

                if (!_tabs.Contains(value)) {
                    return;
                }

                _selectedTab = value;
                SelectedTabChanged?.Invoke(this, _selectedTab);
            }
        }
    }
}
