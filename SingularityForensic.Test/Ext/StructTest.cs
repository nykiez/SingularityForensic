using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Ext {
    [TestClass]
    public class StructTest {
        [TestMethod]
        public void TestSuperBlockSize() {
            Assert.AreEqual( Marshal.SizeOf(typeof(StSuperBlock)),1024);
        }
    }
}
