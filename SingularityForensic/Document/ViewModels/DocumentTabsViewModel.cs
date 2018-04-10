using Prism.Mvvm;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Document.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Document.ViewModels {
    //文件/资源浏览器相关;
    [Export]
    public partial class DocumentTabsViewModel : BindableBase {
        public DocumentTabsViewModel() {
            this._documentService = DocumentService.MainDocumentService;
            RegisterEvents();
        }

        IDocumentService _documentService;
        
        //事件订阅;
        private void RegisterEvents() {
            //订阅添加tab的UI响应;
            PubEventHelper.GetEvent<DocumentAddedEvent>().Subscribe(OnDocumentAdded);
            PubEventHelper.GetEvent<DocumentClosedEvent>().Subscribe(OnDocumentClosed);
            PubEventHelper.GetEvent<SelectedDocumentChangedEvent>().Subscribe(OnSelectedDocumentChanged);
            //订阅关闭的UI响应;
            //_tabService.TabClosed += (sender,tab) => {
            //    if(tab == null) {
            //        return;
            //    }

            //    var preModel = Documents.FirstOrDefault(p => p.DocumentTab == tab);
            //    if(preModel == null) {
            //        return;
            //    }

            //    Documents.Remove(preModel);
            //};

            //订阅清除的UI响应;
            //_tabService.TabsCleared += (sender, tab) => {
            //    Documents.Clear();
            //};

           
           
        }

        /// <summary>
        /// 订阅选择选择文档变更事件;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnSelectedDocumentChanged((IDocument tab, IDocumentService owner) tuple) {
            if(tuple.owner != _documentService) {
                return; 
            }

            var model = Documents.FirstOrDefault(p => p.Document == tuple.tab);
            if (model != null) {
                SelectedDocument = model;
            }
        }

        private void OnDocumentClosed((IDocument tab, IDocumentService owner) tuple) {
            if (tuple.tab == null) {
                return;
            }

            if (tuple.owner != _documentService) {
                return;
            }

            DocumentModel model = Documents.FirstOrDefault(p => p.Document == tuple.tab);
            if(model != null) {
                Documents.Remove(model);
            }
        }

        private void OnDocumentAdded((IDocument tab,IDocumentService owner) tuple) {
            if (tuple.tab == null) {
                return;
            }

            if(tuple.owner != _documentService) {
                return;
            }

            var preModel = Documents.FirstOrDefault(p => p.Document == tuple.tab);
            if (preModel != null) {
                SelectedDocument = preModel;
                return;
            }

            var doc = new DocumentModel(tuple.tab);
            doc.CloseRequest += delegate {
                _documentService.RemoveDocument(doc.Document);
            };
            Documents.Add(doc);
        }

        public ObservableCollection<DocumentModel> Documents { get; set; } = new ObservableCollection<DocumentModel>();

        private DocumentModel _selectedDocument;
        public DocumentModel SelectedDocument {
            get => _selectedDocument;
            set {
                if(_selectedDocument != null) {
                    _selectedDocument.IsActive = false;
                }
                SetProperty(ref _selectedDocument, value);
                _selectedDocument.IsActive = true;
            }
        }

    }

    
}
