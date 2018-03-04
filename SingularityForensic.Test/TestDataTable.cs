using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SingularityForensic.Test {
    [TestClass]
    public class TestDataTable {
        [TestInitialize]
        public void Initialize() {
            _dt = new DataTable();
        }
        private DataTable _dt;
        private Stopwatch watch = new Stopwatch();

        [TestMethod]
        public void TestFilter() {
            var col1 = "col1";
            var col2 = "col2";
            var col3 = "col3";

            _dt.Columns.Add(new DataColumn(col1, typeof(int)));
            _dt.Columns.Add(new DataColumn(col2, typeof(string)));
            _dt.Columns.Add(new DataColumn(col3, typeof(DateTime)));


            for (int i = 0; i < 1000000; i++) {
                var row = _dt.NewRow();
                row[col1] = i;
                row[col3] = DateTime.Now;

                _dt.Rows.Add(row);
            }


            watch.Start();

            var rows = _dt.Select($"{col3} > '2017/01/01' and {col1} > 50000");

            
            //Assert.IsTrue(rows.Length == 1);

            _dt.Rows.Clear();
            
            foreach (var r in rows) {
                _dt.Rows.Add(r);
            }
            watch.Stop();

            Trace.WriteLine(watch.ElapsedMilliseconds);
            //Assert.IsTrue(rows[0] == row);
        }

    }
}
