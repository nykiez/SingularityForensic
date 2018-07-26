using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Common {
    [TestClass]
    public class EnumerableTest {
         private static IEnumerable<int> GetEnumInt() {
            //var sr = new StreamReader("D://CodeMeterRuntime 6.40b.exe");
            var sr = new EntityDis();
            for (int i = 0; i < 10; i++) {
                yield return i;
            }

            sr.Dispose();
        }

        [TestMethod]
        public void TestEnumerableGC() {
            TestEnumrableGCCore();
            
        }
        private void TestEnumrableGCCore() {
            var ints = GetEnumInt();
            foreach (var ins in ints) {
                
            }

            ints = null;
            for (int i = 0; i < 4; i++) {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
        }
       
    }

    public class EntityDis : IDisposable {
        public void Dispose() {
            //dises.Add(this);
        }
        private static readonly List<EntityDis> dises = new List<EntityDis>();
        ~EntityDis() {

        }
    }
}
