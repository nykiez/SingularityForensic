using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Common {
    [TestClass]
    public class TestExtensibleObject {
        [TestMethod]
        public void TestSetAndGetInstance() {
            var extObject = new ExtensibleObject();
            var extName = "HAHAHA";
            var testInt = 1;
            extObject.SetInstance<int>(testInt, extName);
            Assert.AreEqual(testInt, extObject.GetIntance<int>(extName));
            extObject.SetInstance<int>(2, extName);
            Assert.AreNotEqual(testInt, extObject.GetIntance<int>(extName));
        }
    }
}
