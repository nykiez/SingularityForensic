using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.StatusBar;

namespace SingularityForensic.Test.StatusBar {
    [TestClass]
    public class StatusBarServiceTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _statusBarService = ServiceProvider.GetInstance<IStatusBarService>();
            Assert.IsNotNull(_statusBarService);
            _statusBarService.Initialize();
            Assert.AreEqual(_statusBarService.Children.Count(), 1);
        }

        private IStatusBarService _statusBarService;

        [TestMethod]
        public void TestAdd() {
            var newItem = new Mock<IStatusBarObjectItem>();
            _statusBarService.AddStatusBarItem(newItem.Object, GridChildLength.Auto);
            Assert.IsTrue(_statusBarService.Children.Contains(newItem.Object));
        }

        [TestMethod]
        public void TestRemove() {
            var newItem = new Mock<IStatusBarObjectItem>();
            _statusBarService.AddStatusBarItem(newItem.Object, GridChildLength.Auto);
            Assert.IsTrue(_statusBarService.Children.Contains(newItem.Object));
            _statusBarService.RemoveStatusBarItem(newItem.Object);
            Assert.IsTrue(!_statusBarService.Children.Contains(newItem.Object));

        }

        [TestMethod]
        public void TestReport() {
            var textItem = _statusBarService.Children.FirstOrDefault() as IStatusBarTextItem;
            Assert.IsNotNull(textItem);
            var testWord = "Haha";
            _statusBarService.Report(testWord);
            Assert.AreEqual(textItem.Text,testWord);
        }
    }
}
