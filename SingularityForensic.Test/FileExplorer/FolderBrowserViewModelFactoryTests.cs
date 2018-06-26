using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;

namespace SingularityForensic.Test.FileExplorer {
    [TestClass()]
    public class FolderBrowserViewModelFactoryTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _factory = ServiceProvider.GetInstance<IFileExplorerDataContextFactory>();
            Assert.IsNotNull(_factory);
        }

        IFileExplorerDataContextFactory _factory;

        [TestMethod()]
        public void CreateNewTest() {
            var part = FileFactory.CreatePartition(string.Empty);

            var createCatched = false;
            var areEqual = false;
            PubEventHelper.GetEvent<FolderBrowserDataContextCreatedEvent>().Subscribe(tuple => {
                createCatched = true;
                areEqual = tuple.HaveFileCollection == part;
            });

            _factory.CreateFolderBrowserViewModel(part);

            Assert.IsTrue(createCatched);
            Assert.IsTrue(areEqual);
        }
    }
}