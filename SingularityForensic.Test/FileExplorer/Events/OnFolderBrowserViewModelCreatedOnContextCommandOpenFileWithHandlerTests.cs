using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.Events;
using SingularityForensic.Test.App;
using SingularityForensic.Test.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SingularityForensic.Test.FileExplorer.Events {
    [TestClass()]
    public class OnFolderBrowserViewModelCreatedOnContextCommandOpenFileWithHandlerTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            var fileServiceMocker = new Mock<IFileService>();
            fileServiceMocker.Setup(p => p.GetInputStream(It.IsAny<IFile>())).Returns(() => File.OpenRead("D://test.txt"));
            ExportProviderServiceProviderMocker.StaticInstance.SetInstance(fileServiceMocker.Object);

            AppMockers.OpenFileName = "notepad";
        }

        [TestMethod()]
        public void HandleTest() {
            var vm = new Mock<IFolderBrowserViewModel>();
            var cmis = new List<ICommandItem>();
            vm.SetupGet(p => p.ContextCommands).Returns(cmis);
            vm.Setup(p => p.AddContextCommand(It.IsAny<ICommandItem>())).Callback<ICommandItem>(cmi => cmis.Add(cmi));

            var slRow = new Mock<IFileRow>();
            var slFile = new Mock<IFile>();
            slFile.SetupGet(p => p.Name).Returns("1.txt");
            slRow.SetupGet(p => p.File).Returns(slFile.Object);

            vm.SetupGet(p => p.SelectedFile).Returns(slRow.Object);

            var haveFileCollection = new Mock<IHaveFileCollection>();
            
            var sw = new Stopwatch();
            sw.Start();
            
            var handler = new OnFolderBrowserViewModelCreatedOnContextCommandOpenFileWithHandler();
            handler.Handle(vm.Object);

            sw.Stop();

            Assert.AreNotEqual(vm.Object.ContextCommands.Count(), 0);
            vm.Object.ContextCommands.First().Children.ElementAt(1).Command.Execute(null);
            Trace.WriteLine(sw.ElapsedMilliseconds);
            //vm.SetupProperty(p => p)
        }

        [TestMethod()]
        public void CreateOpenFileWithAnotherProCommandTest() {
            Assert.Fail();
        }
    }
}