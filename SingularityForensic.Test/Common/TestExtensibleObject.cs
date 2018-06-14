using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Test.Common {
    [TestClass]
    public class TestExtensibleObject {
        [TestMethod]
        public void TestSetAndGetInstance() {
            var extObject = new ExtensibleObject();
            var extName = "HAHAHA";
            var testInt = 1;
            extObject.SetInstance<int>(testInt, extName);
            Assert.AreEqual(testInt, extObject.GetInstance<int>(extName));
            extObject.SetInstance<int>(2, extName);
            Assert.AreNotEqual(testInt, extObject.GetInstance<int>(extName));
        }
    }
}
