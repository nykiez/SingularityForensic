using System.Collections.ObjectModel;

namespace SingularityForensic.Document.Design {
    public class DocumentTabsDesignViewModel {
        public DocumentTabsDesignViewModel() {
            Documents.Add(new Document { Title = "文档1" });
            Documents.Add(new Document { Title = "文档2" });
            Documents.Add(new Document { Title = "文档3" });
        }
        public ObservableCollection<DocumentBase> Documents { get; set; } = new ObservableCollection<DocumentBase>();
    }
}
