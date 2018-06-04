﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            _handler = new OnInnerFileUnitRightClickRecurBroswingHandler();
        }
        OnInnerFileUnitRightClickRecurBroswingHandler _handler;

        [TestMethod()]
        public void HandleTest() {
            var device = FileSystemService.Current.MountStream(System.IO.File.OpenRead("E://anli/Fat32_Test.img"), "mmp", null, null) as IDevice;
            Assert.IsNotNull(device);
            var unit = TreeUnitFactory.CreateNew(Contracts.FileExplorer.Constants.TreeUnitType_InnerFile);
            unit.SetInstance<IFile>(device.Children.First(), Contracts.FileExplorer.Constants.TreeUnitTag_InnerFile);
            _handler.Handle((unit, Contracts.MainPage.MainTreeService.Current));
            var mainDocService = Contracts.Document.DocumentService.MainDocumentService;
            var docs = mainDocService.CurrentDocuments;
            Assert.AreEqual(docs.Count(), 1);

            var fbDoc = docs.First().GetIntance<IFolderBrowserViewModel>(Contracts.FileExplorer.Constants.DocumentTag_FolderBrowserViewModel);
            Assert.IsNotNull(fbDoc);
            Assert.AreEqual(fbDoc.Files.Count,device.GetInnerFiles().Count());
        }
    }
}