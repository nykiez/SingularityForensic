using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;

namespace SingularityForensic.Test.Documents {
    [TestClass]
    public class DocumentServiceTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            this._documentService = ServiceProvider.Current.GetInstance<IDocumentService>(Contracts.Document.Constants.MainDocumentService);
            Assert.IsNotNull(_documentService);

            
        }

        private IDocumentService _documentService;
    
        [TestMethod]
        public void TestAddTab() {
            var doc = _documentService.AddNewDocument();
            doc.Title = "Test Title";
            doc.UIObject = 1;
            _documentService.AddDocument(doc);

            Assert.AreEqual(_documentService.CurrentDocuments.Count(), 1);
            Assert.AreEqual(_documentService.CurrentDocuments.First(), doc);
        }

        [TestMethod]
        public void TestRemove() {
            TestAddTab();
            var doc = _documentService.CurrentDocuments.First();
            _documentService.RemoveDocument(doc);
            Assert.AreEqual(_documentService.CurrentDocuments.Count(), 0);
        }

        [TestMethod]
        public void TestEnum() {
            var enumDoc = _documentService.AddNewEnumerableDocument();
            enumDoc.AddDocument(new DocumentTabMocker());

            Assert.AreEqual(enumDoc.CurrentDocuments.Count(), 1);

        }

        private class DocumentTabMocker : IDocument {
            public string Title { get; set; }

            public IList<CommandItem> CustomCommands => throw new NotImplementedException();

            public object UIObject {
                get;set;
            }
            public object Tag { get ; set ; }

            public void Dispose() {
                throw new NotImplementedException();
            }

            public TInstance GetIntance<TInstance>(string extName) {
                throw new NotImplementedException();
            }

            public void SetInstance<TInstance>(TInstance instance, string extName) {
                throw new NotImplementedException();
            }
        }
    }
}
