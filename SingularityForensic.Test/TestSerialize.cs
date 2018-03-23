using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SingularityForensic.Test {
    [TestClass]
    public class TestSerialize {
        /// <summary>
        /// 测试委托是否可以被序列化;
        /// </summary>
        [TestMethod]
        public void TestDelegateSerilizable() {
            var ms = new MemoryStream();
            var input = new InnerClass();
            var bf = new BinaryFormatter();
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 100000; i++) {
                bf.Serialize(ms, input);
            }
            sw.Stop();
            Trace.WriteLine($"{nameof(sw)}-{ sw.ElapsedMilliseconds}");
            var ot = new Other();
            input.Getlong = () => ot.Val;
            ms.Position = 0;

            sw.Restart();
            for (int i = 0; i < 100000; i++) {
                var outPut = bf.Deserialize(ms);
                Assert.IsTrue(outPut is InnerClass);
                var res = outPut as InnerClass;
                Assert.AreEqual(res.Val, input.Val);
            }
            sw.Stop();
            Trace.WriteLine($"reopen-{ sw.ElapsedMilliseconds}");
        }

        public class Other {
            public int Val { get; set; } = 2;
        }

        [Serializable]
        public class InnerClass {
            public Func<long> Getlong { get; set; }
            public int Val { get; set; } = 2;
        }
    }
}
