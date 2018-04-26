using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex;

namespace SingularityForensic.Test.Hex {
    [TestClass()]
    public class HexUIServiceTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _hexUIService = ServiceProvider.Current.GetInstance<HexUIReactService>();
            Assert.IsNotNull(_hexUIService);
            _hexUIService.Initialize();

            _mainDocService = DocumentService.MainDocumentService;
            Assert.IsNotNull(_mainDocService);

            _hexService = ServiceProvider.Current.GetInstance<IHexService>();
            Assert.IsNotNull(_hexService);
        }

        HexUIReactService _hexUIService;
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

            ServiceProvider.GetInstance<HexUIServiceImpl>().FindHexValueCommand.CanExecuteChanged += (sender, e) => {
                canExecuteCatched = true;
            };
            
            
            _mainDocService.AddDocument(enumDoc);
            Assert.IsTrue(ServiceProvider.GetInstance<HexUIServiceImpl>().CanHexOperatable);

            Assert.IsTrue(canExecuteCatched);
        }
    }
}