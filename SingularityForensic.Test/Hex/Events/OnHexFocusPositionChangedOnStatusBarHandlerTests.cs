using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.StatusBar;
using SingularityForensic.Hex.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Hex.Events {
    [TestClass()]
    public class OnHexFocusPositionChangedOnStatusBarHandlerTests {
        OnHexFocusPositionChangedOnStatusBarHandler _handler = new OnHexFocusPositionChangedOnStatusBarHandler();
        IStatusBarService _statusbarService;
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _statusbarService = ServiceProvider.GetInstance<IStatusBarService>();
            Assert.IsNotNull(_statusbarService);

            _hexContext = new Mock<IHexDataContext>();
            _handler.Handle(_hexContext.Object);
        }

        private Mock<IHexDataContext> _hexContext;

        [TestMethod()]
        public void FocusPositionTest() {
            var testPosition = 0;
            _hexContext.SetupGet(p => p.FocusPosition).Returns(testPosition);
            _hexContext.Raise(p => p.FocusPositionChanged += null, EventArgs.Empty);

            var positionItem = _statusbarService.Children.FirstOrDefault(p => p.GUID == SingularityForensic.Hex.Constants.StatusBarItemGUID_Position);
            Assert.IsNotNull(positionItem);

            var curCharValueItem = _statusbarService.Children.FirstOrDefault(p => p.GUID == SingularityForensic.Hex.Constants.StatusBarItemGUID_CurCharValue);
            Assert.IsNotNull(curCharValueItem);
        }

        [TestMethod]
        public void LostFocusTest() {
            //_hexContext.Raise(p => p.LostFocus += null, EventArgs.Empty);
            var positionItem = _statusbarService.Children.FirstOrDefault(p => p.GUID == SingularityForensic.Hex.Constants.StatusBarItemGUID_Position);
            Assert.IsNull(positionItem);

            var curCharValueItem = _statusbarService.Children.FirstOrDefault(p => p.GUID == SingularityForensic.Hex.Constants.StatusBarItemGUID_CurCharValue);
            Assert.IsNull(curCharValueItem);
        }
    }
}