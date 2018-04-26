using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Document.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Document {
    [Export(Contracts.Document.Constants.MainDocumentService,typeof(IDocumentService))]
    public partial class MainDocumentService : IDocumentService {
        [ImportingConstructor]
        public MainDocumentService(DocumentTabsViewModel vm) {
            this.VM = vm;
        }

        public void Initialize() {
            RegisterEvents();
            InitializeEventHandlers();
        }

        private void RegisterEvents() {
            //选中文档发生变化时发布事件;
            VM.SelectedDocumentChanged += VM_SelectedDocumentChanged;
        }

        private void InitializeEventHandlers() {
            _documentAddedEventHandlers = ServiceProvider.
                 GetAllInstances<IDocumentAddedEventHandler>().
                 OrderBy(p => p.Sort).
                 ToArray();

            _documentClosedEventHandlers = ServiceProvider.GetAllInstances<IDocumentClosedEventHandler>().
                OrderBy(p => p.Sort).
                 ToArray();
        }
        

        private void VM_SelectedDocumentChanged(object sender, EventArgs e) {
            if(sender != VM) {
                return;
            }

            PubEventHelper.GetEvent<SelectedDocumentChangedEvent>().Publish((SelectedDocument, this));
        }

        public DocumentTabsViewModel VM { get; }
        
        private IEnumerable<IDocumentAddedEventHandler> _documentAddedEventHandlers;
        private IEnumerable<IDocumentClosedEventHandler> _documentClosedEventHandlers;

        /// <summary>
        /// 增加Tab;
        /// </summary>
        /// <param name="doc"></param>
        public void AddDocument(IDocumentBase doc) {
            if (doc == null) {
                throw new ArgumentNullException(nameof(doc));
            }

            if (!(doc is Document document)) {
                throw new InvalidOperationException($"{doc.GetType()} is not a valid type,please user ${nameof(IDocumentService.CreateNewDocument)} instead.");
            }

            if (VM.Documents.FirstOrDefault(p => p == doc) != null) {
                SelectedDocument = doc;
                return;
            }

            PubEventHelper.GetEvent<DocumentAddingEvent>().Publish((doc,this));
            
            document.CloseRequest += Document_CloseRequest;

            VM.Documents.Add(document);
            
            PubEventHelper.PublishEventToHandlers((doc, this as IDocumentService),_documentAddedEventHandlers);
            PubEventHelper.GetEvent<DocumentAddedEvent>().Publish((doc, this));

            SelectedDocument = document;
        }
        
        /// <summary>
        /// 所有的Tab;
        /// </summary>
        public IEnumerable<IDocumentBase> CurrentDocuments => VM.Documents.Select(p => p);

        //关闭所有Tab;
        public void CloseAllDocuments() {
            var cEvg = new CancelEventArgs();
            PubEventHelper.GetEvent<DocumentsClearingEvent>().Publish((cEvg,this));
            if (cEvg.Cancel) {
                return;
            }

            foreach (var doc in VM.Documents) {
                try {
                    PubEventHelper.PublishEventToHandlers((doc as IDocumentBase, this as IDocumentService),_documentClosedEventHandlers);
                    PubEventHelper.GetEvent<DocumentClosedEvent>().Publish((doc, this));
                }
                catch(Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }
            
            VM.Documents.Clear();
            PubEventHelper.GetEvent<DocumentsCleared>().Publish(this);
        }

        /// <summary>
        /// 移除Tab;
        /// </summary>
        /// <param name="doc"></param>
        public void RemoveDocument(IDocumentBase doc) {
            if( doc == null) {
                throw new ArgumentNullException(nameof(doc));
            }

            if (!(doc is Document document)) {
                throw new InvalidOperationException($"{doc.GetType()} is not a valid type,please user ${nameof(IDocumentService.CreateNewDocument)} instead.");
            }
            
            var cEvg = new CancelEventArgs();
            PubEventHelper.GetEvent<DocumentClosingEvent>().Publish((doc, cEvg,this));
            if (cEvg.Cancel) {
                return;
            }

            VM.Documents.Remove(document);

            PubEventHelper.PublishEventToHandlers((doc as IDocumentBase, this as IDocumentService), _documentClosedEventHandlers);
            PubEventHelper.GetEvent<DocumentClosedEvent>().Publish((doc,this));
            
            document.CloseRequest -= Document_CloseRequest;

            if (VM.Documents.Count == 0) {
                SelectedDocument = null;
                PubEventHelper.GetEvent<DocumentsCleared>().Publish(this);
            }
            
#if DEBUG
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            System.GC.Collect();
            System.GC.Collect();
            System.GC.Collect();
#endif
        }

        public IDocument CreateNewDocument() => new Document();
        
        public IDocumentBase SelectedDocument {
            get => VM.SelectedDocument;
            set {
                if(value == null) {
                    PubEventHelper.GetEvent<SelectedDocumentChangedEvent>().Publish((null, this));
                    VM.SelectedDocument = null;
                    return;
                }

                if(!(value is Document document)) {
                    throw new InvalidOperationException($"{value.GetType()} is not a valid type,please use ${nameof(IDocumentService.CreateNewDocument)} instead.");
                }

                VM.SelectedDocument = document;
                PubEventHelper.GetEvent<SelectedDocumentChangedEvent>().Publish((document, this));
                //SelectedTabChanged?.Invoke(this, _selectedTab);
            }
        }
        
        /// <summary>
        /// 创建一个多级的Tab;
        /// </summary>
        /// <returns></returns>
        public IEnumerableDocument CreateNewEnumerableDocument() {
            return new EnumerableDocument();
        }

       

        
    }

    public partial class MainDocumentService {
        /// <summary>
        /// VM中文档发生变化时;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Document_IsActiveChanged(object sender, bool e) {
            if(!(sender is Document docModel)) {
                return;
            }
            
        }

        private void Document_CloseRequest(object sender, EventArgs e) {
            if(!(sender is Document document)) {
                return;
            }

            RemoveDocument(document);
        }
    }
}
