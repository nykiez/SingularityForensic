using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;

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

            PubEventHelper.GetEvent<TabAddingEvent>().Publish(tab);
            _tabs.Add(tab);
            PubEventHelper.GetEvent<TabAddedEvent>().Publish(tab);
        }

        private List<IDocumentTab> _tabs = new List<IDocumentTab>();

        //所有的Tab;
        public IEnumerable<IDocumentTab> CurrentTabs => _tabs.Select(p => p);

        //关闭所有Tab;
        public void CloseAllTabs() {
            var cEvg = new CancelEventArgs();
            PubEventHelper.GetEvent<TabsClearingEvent>().Publish(cEvg);
            if (cEvg.Cancel) {
                return;
            }

            foreach (var tab in _tabs) {
                PubEventHelper.GetEvent<TabClosedEvent>().Publish(tab);
            }
        }

        //关闭Tab
        public void RemoveTab(IDocumentTab tab) {
            if(tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }

            if (!_tabs.Contains(tab)) {
                throw new Exception($"{nameof(tab)} is not inside the list.");
            }
            
            var cEvg = new CancelEventArgs();
            PubEventHelper.GetEvent<TabClosingEvent>().Publish((tab, cEvg));
            if (cEvg.Cancel) {
                return;
            }

            _tabs.Remove(tab);

            PubEventHelper.GetEvent<TabClosedEvent>().Publish(tab);
        }

        private IDocumentTab _selectedTab;

        //public event EventHandler<IDocumentTab> TabAdding;
        //public event EventHandler<IDocumentTab> TabAdded;
        //public event EventHandler<CancelEventArgs> TabsClearing;
        //public event EventHandler TabsCleared;
        //public event EventHandler<(IDocumentTab tab, CancelEventArgs e)> TabClosing;
        //public event EventHandler<IDocumentTab> TabClosed;
        //public event EventHandler<IDocumentTab> SelectedTabChanged;

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
                PubEventHelper.GetEvent<SelectedTabChangedEvent>().Publish(_selectedTab);
                //SelectedTabChanged?.Invoke(this, _selectedTab);
            }
        }
    }
}
