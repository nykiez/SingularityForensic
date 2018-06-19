using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using System.Linq;

namespace SingularityForensic.Test.FileExplorer {
    [TestClass()]
    public class ViewerSeviceImplTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _viewerService = ServiceProvider.GetInstance<IViewerService>();
            Assert.IsNotNull(_viewerService);
        }

        private IViewerService _viewerService;

        [TestMethod()]
        public void AddViewerTest() {
            _viewerService.AddViewer("pornhub", "D://mp4info.exe");
            Assert.IsNotNull(_viewerService.GetAllViewers().FirstOrDefault(p => p.Value.viewerName == "pornhub"));
            Assert.AreEqual(_viewerService.GetAllViewers().Count(), 3);
            _viewerService.Reset();
        }

        [TestMethod]
        public void ResetTest() {
            _viewerService.Reset();
            Assert.AreEqual(_viewerService.GetAllViewers().Count(), 2);
        }
    }
}