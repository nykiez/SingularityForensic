using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.TreeView;
using System.Linq;

namespace SingularityForensic.Test.Casing {
    [TestClass()]
    public class CaseUIServiceTests {
        public CaseUIServiceTests() {
            _csServiceTest = new CaseServiceTest();
            _csServiceTest.Initialize();

            _csUIService = ServiceProvider.Current?.GetInstance<CaseUIService>();
            Assert.IsNotNull(_csUIService);
            
            _nodeService = ServiceProvider.Current?.GetInstance<ITreeService>();
            Assert.IsNotNull(_nodeService);

            _csUIService.Initialize();
            
        }

        private CaseServiceTest _csServiceTest;
        private CaseUIService _csUIService;
        private ITreeService _nodeService;

        [TestMethod()]
        public void TestLoadCase() {
            _csServiceTest.CreateAndLoadCase();
            
            Assert.AreEqual(_nodeService.CurrentUnits.Count(), 1);
        }

        [TestMethod]
        public void TestLoadEvidence() {
            _csServiceTest.LoadCase();

            Assert.AreEqual(_nodeService.CurrentUnits.Count(), 1);

            var unit = _nodeService.CurrentUnits.First();
            Assert.AreEqual(unit.Children.Count, 1);
            Assert.AreEqual(unit.GetIntance<Case>(Contracts.Casing.Constants.TreeUnitType_Case), Contracts.Casing.CaseService.Current.CurrentCase);
        }

        [TestMethod]
        public void TestCloseCase() {
            _csServiceTest.LoadCase();
            _csServiceTest.CloseCase();

            Assert.AreEqual(_nodeService.CurrentUnits.Count(), 0);
        }
    }
}