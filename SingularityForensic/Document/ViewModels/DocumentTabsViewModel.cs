using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Document.ViewModels {
    //文件/资源浏览器相关;
    [Export]
    public partial class DocumentTabsViewModel : BindableBase {
        public DocumentTabsViewModel() {
            
        }
        
        public ObservableCollection<Document> Documents { get; set; } = new ObservableCollection<Document>();


        private Document _selectedDocument;
        public Document SelectedDocument {
            get => _selectedDocument;
            set {
                SetProperty(ref _selectedDocument, value);
                SelectedDocumentChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler SelectedDocumentChanged;
    }

    
}
