using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Document.ViewModels;
using SingularityForensic.Document.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.Document {
    class EnumerableDocument : IEnumerableDocument {
        public EnumerableDocument() {
            _uiObject = ServiceProvider.Current.GetInstance<FrameworkElement>(Constants.EnumerableTabView);
            if (_uiObject == null) {
                throw new AggregateException($"No export has been found:{nameof(_uiObject)}.");
            }

            _vm = new EnumerableTabViewModel();

            _vm.SelectedDocumentChanged += OnSelectedTabChanged;
            _uiObject.DataContext = _vm;
        }

        public IEnumerable<IDocument> Children => _vm.DocumentTabs.Select(p => p);
        private void OnSelectedTabChanged(object sender, IDocument e) {
            PubEventHelper.GetEvent<SelectedTabChangedEvent>().Publish((e, this));
        }

        public string Title { get; set; }

        private ObservableCollection<CommandItem> _customCommands = new ObservableCollection<CommandItem>();
        public IList<CommandItem> CustomCommands => _customCommands;

        private FrameworkElement _uiObject;
        public object UIObject {
            get => _uiObject;
            set => throw new InvalidOperationException($"The UIObject of {nameof(EnumerableDocument)} can't be set.");
        }

        private EnumerableTabViewModel _vm;
        public object MainUIObject {
            get => _vm.MainView;
            set => _vm.MainView = value;
        }

        public object Tag { get; set; }

        public IEnumerable<IDocument> CurrentDocuments => _vm.DocumentTabs.Select(p => p);

        public IDocument SelectedDocument {
            get => _vm.SelectedDocument;
            set => _vm.SelectedDocument = value;
        }

        public void Dispose() {
            _vm.SelectedDocumentChanged -= OnSelectedTabChanged;
        }

        public void AddDocument(IDocument tab) {
            if(tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }

            _vm.DocumentTabs.Add(tab);
        }

        public void RemoveDocument(IDocument tab) {
            if(tab == null) {
                throw new ArgumentNullException(nameof(tab));
            }

            _vm.DocumentTabs.Remove(tab);
        }

        public IDocument CreateNewDocument() {
            return new Document();
        }

        public IEnumerableDocument CreateEnumerableDocument() {
            return new EnumerableDocument();
        }

        public void CloseAllDocuments() {
            _vm.DocumentTabs.Clear();
        }
    }
       
}
