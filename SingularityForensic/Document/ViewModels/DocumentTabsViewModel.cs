using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Document.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Document.ViewModels {
    //文件/资源浏览器相关;
    [Export]
    public partial class DocumentTabsViewModel : BindableBase {
        [ImportingConstructor]
        public DocumentTabsViewModel(IDocumentTabService tabService) {
            this._tabService = tabService;
            RegisterEvents();
        }

        IDocumentTabService _tabService;
        
        //事件订阅;
        private void RegisterEvents() {
            //订阅添加tab的UI响应;
            _tabService.TabAdded += (sender, tab) => {
                if (tab == null) {
                    return;
                }

                var preModel = Documents.FirstOrDefault(p => p.DocumentTab == tab);
                if (preModel != null) {
                    SelectedDocument = preModel;
                    return;
                }

                Documents.Add(new DocumentModel(tab));
            };
            
            //订阅关闭的UI响应;
            _tabService.TabClosed += (sender,tab) => {
                if(tab == null) {
                    return;
                }

                var preModel = Documents.FirstOrDefault(p => p.DocumentTab == tab);
                if(preModel == null) {
                    return;
                }

                Documents.Remove(preModel);
            };

            //订阅清除的UI响应;
            _tabService.TabsCleared += (sender, tab) => {
                Documents.Clear();
            };

            //订阅选择选择文档变更事件;
            _tabService.SelectedTabChanged += (sender, tab) => {
                var tabModel = Documents.FirstOrDefault(p => p.DocumentTab == tab);
                if(tabModel == null) {
                    return;
                }

                SelectedDocument = tabModel;
            };
        }

        public ObservableCollection<DocumentModel> Documents { get; set; } = new ObservableCollection<DocumentModel>();

        private DocumentModel _selectedDocument;
        public DocumentModel SelectedDocument {
            get => _selectedDocument;
            set => SetProperty(ref _selectedDocument, value);
        }

    }

    
}
