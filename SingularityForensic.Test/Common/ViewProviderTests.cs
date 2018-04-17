using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Common;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Test.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Common {
    [TestClass()]
    public class ViewProviderTests {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            ViewProvider.SetViewProvider(new ServiceProviderViewProvider(ServiceProvider.Current));
            Assert.IsNotNull(_viewProvider = ViewProvider.Current);
        }
        IViewProvider _viewProvider;

        [TestMethod()]
        public void SetViewProviderTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetViewTest() {
            var hexView = _viewProvider.GetView(Contracts.Hex.Constants.HexView);
        }
    }
}