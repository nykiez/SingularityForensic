using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Document.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SingularityForensic.Document {
    class EnumerableDocument : Document,IEnumerableDocument {
        public EnumerableDocument() {
            _vm = new EnumerableTabViewModel();
            _vm.SelectedDocumentChanged += OnSelectedTabChanged;

            _uiObject = ViewProvider.GetView(Constants.EnumerableTabView) as FrameworkElement;

            if (_uiObject != null) {
                _uiObject.DataContext = _vm;
            }
        }

        public IEnumerable<IDocumentBase> Children => _vm.DocumentTabs.Select(p => p);
        private void OnSelectedTabChanged(object sender, IDocumentBase e) {
            PubEventHelper.GetEvent<SelectedDocumentChangedEvent>().Publish((e, this));
        }

        
        private FrameworkElement _uiObject;
        public override object UIObject {
            get => _uiObject;
            set => throw new InvalidOperationException($"The UIObject of {nameof(EnumerableDocument)} can't be set.");
        }

        private EnumerableTabViewModel _vm;
        public object MainUIObject {
            get => _vm.MainView;
            set => _vm.MainView = value;
        }
        
        public IEnumerable<IDocumentBase> CurrentDocuments => _vm.DocumentTabs.Select(p => p);

        public IDocumentBase SelectedDocument {
            get => _vm.SelectedDocument;
            set => _vm.SelectedDocument = value;
        }

        public override void Dispose() {
            _vm.SelectedDocumentChanged -= OnSelectedTabChanged;
        }

        public void AddDocument(IDocumentBase tab) {
            if(tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }

            PubEventHelper.GetEvent<DocumentAddingEvent>().Publish((tab, this));
            _vm.DocumentTabs.Add(tab);
            PubEventHelper.GetEvent<DocumentAddedEvent>().Publish((tab, this));
        }

        public void RemoveDocument(IDocumentBase tab) {
            if(tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }

            var cEvg = new CancelEventArgs();
            PubEventHelper.GetEvent<DocumentClosingEvent>().Publish((tab, cEvg, this));
            if (cEvg.Cancel) {
                return;
            }
            _vm.DocumentTabs.Remove(tab);
            PubEventHelper.GetEvent<DocumentClosedEvent>().Publish((tab, this));

            if(_vm.DocumentTabs.Count == 0) {
                PubEventHelper.GetEvent<DocumentsCleared>().Publish(this);
            }
        }

        public IDocument CreateNewDocument() {
            return new Document();
        }

        public IEnumerableDocument CreateNewEnumerableDocument() {
            return new EnumerableDocument();
        }

        public void CloseAllDocuments() {
            var cEvg = new CancelEventArgs();
            PubEventHelper.GetEvent<DocumentsClearingEvent>().Publish((cEvg, this));
            if (cEvg.Cancel) {
                return;
            }

            foreach (var doc in _vm.DocumentTabs) {
                try {
                    PubEventHelper.GetEvent<DocumentClosedEvent>().Publish((doc, this));
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            _vm.DocumentTabs.Clear();
            PubEventHelper.GetEvent<DocumentsCleared>().Publish(this);
        }
    }
       
}
