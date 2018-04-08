using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Data {
    [TestClass]
    public class NotifybleDataTableTest {
        NotifybleDataTable dt = new NotifybleDataTable();
        [TestMethod]
        public void TestClearRaised() {
            var row = dt.NewRow();
            dt.CollectionChanged += delegate {

            };
            dt.Rows.Add(row);
            dt.Rows.Clear();
            
        }
    }
}
