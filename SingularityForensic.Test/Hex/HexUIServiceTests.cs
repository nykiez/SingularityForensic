using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Hex {
    [TestClass()]
    public class HexUIServiceTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _hexUIService = ServiceProvider.Current.GetInstance<HexUIService>();
            Assert.IsNotNull(_hexUIService);
            _hexUIService.Initialize();

            _mainDocService = DocumentService.MainDocumentService;
            Assert.IsNotNull(_mainDocService);

            _hexService = ServiceProvider.Current.GetInstance<IHexService>();
            Assert.IsNotNull(_hexService);
        }

        HexUIService _hexUIService;
        IDocumentService _mainDocService;
        IHexService _hexService;

        [TestMethod()]
        public void CanHexOperatableTest() {
            var canExecuteCatched = false;
            var enumDoc = _mainDocService.CreateNewEnumerableDocument();
            var hexDoc = enumDoc.CreateNewDocument();
            enumDoc.AddDocument(hexDoc);
            enumDoc.SelectedDocument = hexDoc;
            var context = _hexService.CreateNewHexDataContext(null);
            hexDoc.SetInstance(context, Contracts.Hex.Constants.Tag_HexDataContext);

            _hexUIService.FindHexValueCommand.CanExecuteChanged += (sender, e) => {
                canExecuteCatched = true;
            };
            
            
            _mainDocService.AddDocument(enumDoc);
            Assert.IsTrue(_hexUIService.CanHexOperatable);

            Assert.IsTrue(canExecuteCatched);
        }
    }
}