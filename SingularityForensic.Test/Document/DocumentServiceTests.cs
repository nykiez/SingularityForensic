using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Helpers;
using System.Linq;

namespace SingularityForensic.Test.Document {
    [TestClass()]
    public class DocumentServiceTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            Assert.IsNotNull( _documentService = 
                ServiceProvider.Current.GetInstance<IDocumentService>(
                    Contracts.Document.Constants.MainDocumentService
                ) as SingularityForensic.Document.MainDocumentService
            );
        }

        private SingularityForensic.Document.MainDocumentService _documentService;
        [TestMethod()]
        public void DocumentServiceTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddDocumentTest() {
            var newDoc = _documentService.CreateNewDocument();
            var addingCatched = false;
            var addedCathed = true;
            PubEventHelper.GetEvent<DocumentAddingEvent>().Subscribe(tuple => {
                addingCatched = true;
                Assert.AreEqual(tuple.tab, newDoc);
            });
            PubEventHelper.GetEvent<DocumentAddedEvent>().Subscribe(tuple => {
                addedCathed = true;
                Assert.AreEqual(tuple.tab, newDoc);
            });

            _documentService.AddDocument(newDoc);

            Assert.IsTrue(addingCatched);
            Assert.IsTrue(addedCathed);

            Assert.AreEqual(_documentService.CurrentDocuments.Count(), 1);
            Assert.AreEqual(_documentService.SelectedDocument, newDoc);
            Assert.AreEqual(_documentService.CurrentDocuments.First(), newDoc);

            Assert.AreEqual(_documentService.VM.Documents.Count, 1);
            Assert.AreEqual(_documentService.VM.Documents.First(), newDoc);
        }

        [TestMethod()]
        public void CloseAllDocumentsTest() {
            
        }

        [TestMethod()]
        public void RemoveDocumentTest() {
            AddDocumentTest();
            var removedDoc = _documentService.SelectedDocument;

            RegisterCloseEvents();
            PubEventHelper.GetEvent<DocumentClosingEvent>().Subscribe(tuple => {
                Assert.AreEqual(tuple.tab, removedDoc);
            });

            PubEventHelper.GetEvent<DocumentClosedEvent>().Subscribe(tuple => {
                Assert.AreEqual(tuple.doc, removedDoc);
            });
            
            _documentService.RemoveDocument(_documentService.CurrentDocuments.First());

            CheckClosecatched();
            CheckClear();
        }

        bool closingCatched = false;
        bool closedCatched = false;

        /// <summary>
        /// 注册关闭事件;
        /// </summary>
        private void RegisterCloseEvents() {
            PubEventHelper.GetEvent<DocumentClosingEvent>().Subscribe(tuple => {
                closingCatched = true;
            });

            PubEventHelper.GetEvent<DocumentClosedEvent>().Subscribe(tuple => {
                closedCatched = true;            });
        }
        
        private void CheckClosecatched() {
            Assert.IsTrue(closingCatched);
            Assert.IsTrue(closedCatched);
        }

        private void CheckClear() {
            Assert.AreEqual(_documentService.CurrentDocuments.Count(), 0);
            Assert.AreEqual(_documentService.SelectedDocument, null);

            Assert.AreEqual(_documentService.VM.Documents.Count, 0);
        }

        [TestMethod()]
        public void CloseDocumentTest() {
            AddDocumentTest();

            RegisterCloseEvents();

            _documentService.VM.Documents[0].CloseCommand.Execute();
            
            CheckClosecatched();
            CheckClear();
        }

        [TestMethod()]
        public void CreateNewEnumerableDocumentTest() {
            Assert.Fail();
        }
    }
}