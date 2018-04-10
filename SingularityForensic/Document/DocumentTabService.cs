using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Document {
    [Export(Contracts.Document.Constants.MainDocumentService,typeof(IDocumentService))]
    class DocumentTabService : IDocumentService {
        //增加Tab;
        public void AddDocument(IDocument tab) {
            if(tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }

            if (_tabs.Contains(tab)) {
                SelectedDocument = tab;
                return;
            }

            PubEventHelper.GetEvent<DocumentAddingEvent>().Publish((tab,this));
            _tabs.Add(tab);
            PubEventHelper.GetEvent<DocumentAddedEvent>().Publish((tab, this));
        }

        private List<IDocument> _tabs = new List<IDocument>();

        //所有的Tab;
        public IEnumerable<IDocument> CurrentDocuments => _tabs.Select(p => p);

        //关闭所有Tab;
        public void CloseAllDocuments() {
            var cEvg = new CancelEventArgs();
            PubEventHelper.GetEvent<DocumentsClearingEvent>().Publish((cEvg,this));
            if (cEvg.Cancel) {
                return;
            }

            foreach (var tab in _tabs) {
                try {
                    tab.Dispose();
                    PubEventHelper.GetEvent<DocumentClosedEvent>().Publish((tab, this));
                }
                catch(Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            _tabs.Clear();
        }

        /// <summary>
        /// 移除Tab;
        /// </summary>
        /// <param name="tab"></param>
        public void RemoveDocument(IDocument tab) {
            if( tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }

            if (!_tabs.Contains(tab)) {
                throw new Exception($"{nameof(tab)} is not inside the list.");
            }
            
            var cEvg = new CancelEventArgs();
            PubEventHelper.GetEvent<DocumentClosingEvent>().Publish((tab, cEvg,this));
            if (cEvg.Cancel) {
                return;
            }

            _tabs.Remove(tab);

            try {
                tab.Dispose();
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            PubEventHelper.GetEvent<DocumentClosedEvent>().Publish((tab,this));
         
            
        }

        public IDocument AddNewDocument() => new Document();

        private IDocument _selectedTab;
        
        public IDocument SelectedDocument {
            get => _selectedTab;
            set {
                if(value == null) {
                    throw new ArgumentNullException(nameof(SelectedDocument));
                }

                if (!_tabs.Contains(value)) {
                    return;
                }

                _selectedTab = value;
                PubEventHelper.GetEvent<SelectedDocumentChangedEvent>().Publish((_selectedTab,this));
                //SelectedTabChanged?.Invoke(this, _selectedTab);
            }
        }

        /// <summary>
        /// 创建一个多级的Tab;
        /// </summary>
        /// <returns></returns>
        public IEnumerableDocument AddNewEnumerableDocument() {
            return new EnumerableDocument();
        }
    }
}
