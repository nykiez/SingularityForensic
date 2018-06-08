using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
