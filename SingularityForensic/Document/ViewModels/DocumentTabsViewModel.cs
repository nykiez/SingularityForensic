using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace SingularityForensic.Document.ViewModels {
    //文件/资源浏览器相关;
    [Export]
    public partial class DocumentTabsViewModel : BindableBase {
        public DocumentTabsViewModel() {
            
        }
        
        public ObservableCollection<DocumentBase> Documents { get; set; } = new ObservableCollection<DocumentBase>();


        private DocumentBase _selectedDocument;
        public DocumentBase SelectedDocument {
            get => _selectedDocument;
            set {
                SetProperty(ref _selectedDocument, value);
                SelectedDocumentChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler SelectedDocumentChanged;
    }

    
}
