using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.FileExplorer.Events;
using System.Linq;

namespace SingularityForensic.Test.FileExplorer.Events {
    [TestClass()]
    public class OnInnerFileUnitRightClickRecurBroswingEventHandlerTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _handler = new OnFileUnitAddedRecurBroswingHandler();
        }
        OnFileUnitAddedRecurBroswingHandler _handler;

        [TestMethod()]
        public void HandleTest() {
            var device = FileSystemService.Current.MountStream(System.IO.File.OpenRead("E://anli/Fat32_Test.img"), "mmp", null, null) as IDevice;
            Assert.IsNotNull(device);
            var unit = TreeUnitFactory.CreateNew(SingularityForensic.Contracts.FileExplorer.Constants.TreeUnitType_InnerFile);
            unit.SetInstance<IFile>(device.Children.First(), SingularityForensic.Contracts.FileExplorer.Constants.TreeUnitTag_InnerFile);
            _handler.Handle((unit, SingularityForensic.Contracts.MainPage.MainTreeService.Current));
            var mainDocService = SingularityForensic.Contracts.Document.DocumentService.MainDocumentService;
            var docs = mainDocService.CurrentDocuments;
            Assert.AreEqual(docs.Count(), 1);

            var fbDoc = docs.First().GetInstance<IFolderBrowserViewModel>(SingularityForensic.Contracts.FileExplorer.Constants.DocumentTag_FolderBrowserViewModel);
            Assert.IsNotNull(fbDoc);
            Assert.AreEqual(fbDoc.Files.Count(),device.GetInnerFiles().Count());
        }
    }
}