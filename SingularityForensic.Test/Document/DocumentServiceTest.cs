using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Document.Events;
using SingularityForensic.Contracts.Helpers;

namespace SingularityForensic.Test.Documents {
    [TestClass]
    public partial class DocumentServiceTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            this._documentService = ServiceProvider.Current.GetInstance<IDocumentService>(SingularityForensic.Contracts.Document.Constants.MainDocumentService);
            Assert.IsNotNull(_documentService);
            
            
        }

        private IDocumentService _documentService;

        [TestMethod]
        public void TestAddTab() {
            var addingCatched = false;
            var addedCatched = false;

            PubEventHelper.GetEvent<DocumentAddingEvent>().Subscribe(tuple => {
                addingCatched = true;
            });
            PubEventHelper.GetEvent<DocumentAddingEvent>().Subscribe(tuple => {
                addedCatched = true;
            });
            
            var doc = _documentService.CreateNewDocument();
            doc.Title = "Test Title";
            doc.UIObject = 1;
            _documentService.AddDocument(doc);

            Assert.AreEqual(_documentService.CurrentDocuments.Count(), 1);
            Assert.AreEqual(_documentService.CurrentDocuments.First(), doc);

            Assert.IsTrue(addingCatched);
            Assert.IsTrue(addedCatched);
        }

        [TestMethod]
        public void TestRemove() {
            TestAddTab();
            var closedCatched = false;
            var closingCatched = false;

            var doc = _documentService.CurrentDocuments.First();
            PubEventHelper.GetEvent<DocumentClosedEvent>().Subscribe(tuple => {
                closedCatched = true;
                Assert.AreEqual(tuple.doc, doc);
            });
            PubEventHelper.GetEvent<DocumentClosingEvent>().Subscribe(tuple => {
                closingCatched = true;
                Assert.AreEqual(tuple.tab, doc);
            });

            _documentService.RemoveDocument(doc);

            Assert.AreEqual(_documentService.CurrentDocuments.Count(), 0);

            Assert.IsTrue(closedCatched);
            Assert.IsTrue(closingCatched);
        }

        [TestMethod]
        public void TestEnum() {
            var enumDoc = _documentService.CreateNewEnumerableDocument();
            enumDoc.AddDocument(new DocumentTabMocker());

            Assert.AreEqual(enumDoc.CurrentDocuments.Count(), 1);

        }

        private class DocumentTabMocker : IDocument {
            public string Title { get; set; }

            public IList<ICommandItem> CustomCommands => throw new NotImplementedException();

            public object UIObject {
                get; set;
            }
            public object Tag { get; set; }

            public void Dispose() {
                throw new NotImplementedException();
            }

            public TInstance GetInstance<TInstance>(string extName) {
                throw new NotImplementedException();
            }

            public void SetInstance<TInstance>(TInstance instance, string extName) {
                throw new NotImplementedException();
            }
        }
    }

    /// <summary>
    /// 测试多重文档;
    /// </summary>
    public partial class DocumentServiceTest {
        private IEnumerableDocument _enumDoc;
        [TestMethod]
        public void TestAddDoc() {
            _enumDoc = _documentService.CreateNewEnumerableDocument();
            var doc = _enumDoc.CreateNewDocument();
            var addingCatched = false;
            var addedCatched = false;

            PubEventHelper.GetEvent<DocumentAddingEvent>().Subscribe(tuple => {
                addingCatched = true;
            });

            PubEventHelper.GetEvent<DocumentAddingEvent>().Subscribe(tuple => {
                addedCatched = true;
            });

            _enumDoc.AddDocument(doc);

            Assert.IsTrue(addingCatched);
            Assert.IsTrue(addedCatched);
        }
    }
}
