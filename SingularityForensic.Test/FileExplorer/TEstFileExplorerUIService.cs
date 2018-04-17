using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using SingularityForensic.Test.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.FileExplorer {
    [TestClass]
    public class TestFileExplorerUIService {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();

            _fileExplorerUIService = ServiceProvider.Current.GetInstance<IFileExplorerUIReactService>();
            Assert.IsNotNull(_fileExplorerUIService);
            _fileExplorerUIService.Initialize();
            
            _docService = DocumentService.MainDocumentService;
            Assert.IsNotNull(_docService);

            _fsService = ServiceProvider.Current.GetInstance<IFileSystemService>();
            Assert.IsNotNull(_fsService);
            _fsService.Initialize();

            AppMockers.OpenFileName = "E://anli/Fat32_Test.img";
        }

        private IFileExplorerUIReactService _fileExplorerUIService;
        private IDocumentService _docService;
        private IFileSystemService _fsService;

        [TestMethod]
        public void TestOnTreeUnitAdded() {
            var csUnit = TreeUnitFactory.CreateNew(Contracts.Casing.Constants.CaseEvidenceUnit);
            var csEvidence = new CaseEvidence(new string[] { }, string.Empty, string.Empty);
            var file = _fsService.MountStream(File.OpenRead(AppMockers.OpenFileName), csEvidence.Name, csEvidence.XElem, null);
            csUnit.SetInstance(csEvidence, Contracts.Casing.Constants.TreeUnitTag_CaseEvidence);

            PubEventHelper.GetEvent<TreeUnitAddedEvent>().Publish((csUnit, MainTreeService.Current));

            Assert.AreEqual(csUnit.Children.Count, 1);
        }

        [TestMethod]
        public void TestClickOnFileSystemUnit() {
            
            //var file = FSService.Current.MountStream(System.IO.File.OpenRead($"E://anli/{imgFileName}"), imgFileName, null, null);
            //var unit = new TreeUnit(SingularityForensic.FileExplorer.Constants.FileSystemTreeUnit);
            //unit.SetInstance(file, SingularityForensic.Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
            //PubEventHelper.GetEvent<TreeUnitClickEvent>().Publish(unit);
            //PubEventHelper.GetEvent<TreeUnitClickEvent>().Publish(unit);
            //PubEventHelper.GetEvent<TreeUnitClickEvent>().Publish(unit);

            //Assert.AreEqual(_docService.CurrentDocuments.Count(), 1);
            //Assert.AreEqual(_docService.CurrentDocuments.First().Title, imgFileName);
        }

        
    }
}
