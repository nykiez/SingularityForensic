using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.FileExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.FileExplorer {
    [TestClass()]
    public class FolderBrowserViewModelFactoryTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _factory = ServiceProvider.GetInstance<IFileExplorerViewModelFactory>();
            Assert.IsNotNull(_factory);
        }

        IFileExplorerViewModelFactory _factory;

        [TestMethod()]
        public void CreateNewTest() {
            var part = FileFactory.CreatePartition(string.Empty);

            var createCatched = false;
            var areEqual = false;
            PubEventHelper.GetEvent<FolderBrowserViewModelCreatedEvent>().Subscribe(tuple => {
                createCatched = true;
                areEqual = tuple.HaveFileCollection == part;
            });

            _factory.CreateFolderBrowserViewModel(part);

            Assert.IsTrue(createCatched);
            Assert.IsTrue(areEqual);
        }
    }
}