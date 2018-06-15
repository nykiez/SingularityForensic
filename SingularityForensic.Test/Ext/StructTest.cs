using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Ext;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Ext {
    [TestClass]
    public class StructTest {
        [TestMethod]
        public void TestSuperBlock() {
            Assert.AreEqual( Marshal.SizeOf(typeof(StSuperBlock)),1024);
            PrintStructFields<StSuperBlock>(Constants.ExtSuperBlockFieldPrefix);
        }
        [TestMethod]
        public void PrintDescgroup() {
            PrintStructFields<StExtGroupDesc>(Constants.ExtGroupDescFieldPrefix);
        }
        private void PrintStructFields<T>(string prefix) {
            var tp = typeof(T);
            foreach (var fieldInfo in tp.GetFields()) {
                Trace.WriteLine($"<sys:String x:Key=\"{ prefix}{ fieldInfo.Name}\"></sys:String>");
            }
        }

    }
}
