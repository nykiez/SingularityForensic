using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test {
    
    [TestClass]
    public class TestPath {
        [TestMethod]
        public void TestDirect() {
            var dir = Path.GetDirectoryName("P://dat").Replace('\\','/');
            Assert.AreEqual(dir, "P:/");
        }
    }
}
