using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Document.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SingularityForensic.Document {
    class EnumerableDocument : DocumentBase,IEnumerableDocument {
        public EnumerableDocument() {
            _vm = new EnumerableTabViewModel();
            _vm.SelectedDocumentChanged += OnSelectedTabChanged;

            UIObject = ViewProvider.CreateView(Constants.EnumerableTabView, _vm);
        }

        public IEnumerable<IDocumentBase> Children => _vm.DocumentTabs.Select(p => p);
        private void OnSelectedTabChanged(object sender, IDocumentBase e) {
            CommonEventHelper.GetEvent<SelectedDocumentChangedEvent>().Publish((e, this));
        }

        
        public override object UIObject {
            get;
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

            CommonEventHelper.GetEvent<DocumentAddingEvent>().Publish((tab, this));
            _vm.DocumentTabs.Add(tab);
            CommonEventHelper.GetEvent<DocumentAddedEvent>().Publish((tab, this));
        }

        public void RemoveDocument(IDocumentBase tab) {
            if(tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }

            var cEvg = new CancelEventArgs();
            CommonEventHelper.GetEvent<DocumentClosingEvent>().Publish((tab, cEvg, this));
            if (cEvg.Cancel) {
                return;
            }
            _vm.DocumentTabs.Remove(tab);
            CommonEventHelper.PublishEventToHandlers((tab, this as IDocumentService),
                ServiceProvider.GetAllInstances<IDocumentClosedEventHandler>().OrderBy(p => p.Sort));
            CommonEventHelper.GetEvent<DocumentClosedEvent>().Publish((tab, this));

            if(_vm.DocumentTabs.Count == 0) {
                CommonEventHelper.GetEvent<DocumentsCleared>().Publish(this);
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
            CommonEventHelper.GetEvent<DocumentsClearingEvent>().Publish((cEvg, this));
            if (cEvg.Cancel) {
                return;
            }



            foreach (var doc in _vm.DocumentTabs) {
                try {
                    CommonEventHelper.PublishEventToHandlers((doc, this as IDocumentService),
                        ServiceProvider.GetAllInstances<IDocumentClosedEventHandler>().OrderBy(p => p.Sort));
                    CommonEventHelper.GetEvent<DocumentClosedEvent>().Publish((doc, this));
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            _vm.DocumentTabs.Clear();
            CommonEventHelper.GetEvent<DocumentsCleared>().Publish(this);
        }

        public void Initialize() {
            
        }
    }
       
}
