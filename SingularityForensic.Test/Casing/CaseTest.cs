using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Casing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Casing {
    [TestClass]
    public class CaseTest {
        [TestMethod]
        public void TestCreateAndLoad() {
            var csName = "fuckyou";
            var csPath = "E://Cases/Case002";
            var cs = new Case(csPath,csName);
            cs.Save();
            Assert.IsNotNull(cs.GUID);

            var cs2 = Case.LoadFrom($"{csPath}/{csName}/{csName}{Constants.CaseFileExtention}");

            var props = cs2.GetType().GetProperties();
            foreach (var prop in props) {
                var val = prop.GetValue(cs);
                var val2 = prop.GetValue(cs2);
                if (prop.PropertyType.IsClass) {
                    continue;
                }
                if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType)) {
                    continue;
                }
                Assert.AreEqual(val, val2);
            }
        }
    }
}
