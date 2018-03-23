using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.FileExplorer {
    public class Model {
        public int Int1 { get; set; }
        public int Int2 { get; set; }
        public string Int3 { get; set; }
        public string Int4 { get; set; }
        public string Int7 { get; set; }

        public int GetInt(string name) {
            if(name == nameof(Int1)) {
                return Int1;
            }
            else if(name == nameof(Int2)){
                return Int2;
            }
            return 0;
        }
    }

    [TestClass]
    public class TestDataTable {
        [TestInitialize]
        public void Initialize() {
            var rand = new Random();
            _dt.Columns.Add(new DataColumn(IntCol1, typeof(int)));
            _dt.Columns.Add(new DataColumn(IntCol2, typeof(int)));
            for (int i = 0; i < 1000000; i++) {
                var int1 = rand.Next(1000);
                var int2 = rand.Next(1000);
                var row = _dt.NewRow();
                row[IntCol1] = int1;
                row[IntCol2] = int2;
                var model = new Model {
                    Int1 = int1,
                    Int2 = int2
                };

                list.Add(model);
                models.Add(model);
                _dt.Rows.Add(row);
            }
        }
        List<object> list = new List<object>();
        List<Model> models = new List<Model>();
        List<object> obs = new List<object>();
        List<DataRow> _rows = new List<DataRow>();

        DataTable _dt = new DataTable();
        const string IntCol1 = nameof(IntCol1);
        const string IntCol2 = nameof(IntCol2);
        Stopwatch sw = new Stopwatch();
        [TestMethod]
        public void TestFilterPerformance() {
            var devide = 700;
            var dt1 = _dt.Copy();
            obs.Clear();

            sw.Start();
            var rm1List = dt1.Select($"{IntCol1} <= {devide} or {IntCol2} >= {devide}");
            foreach (var row in rm1List) {
                dt1.Rows.Remove(row);
            }
            sw.Stop();
            Trace.WriteLine($"DataTable Select:{sw.ElapsedMilliseconds} - {dt1.Rows.Count}");
            
            

            var tp = typeof(Model);
            var col1Info = tp.GetProperty(nameof(Model.Int1));
            var col2Info = tp.GetProperty(nameof(Model.Int2));
            obs.Clear();
            sw.Restart();
            var rows2 = list.Where(p => (int)col1Info.GetValue(p) > devide && (int)col2Info.GetValue(p) < devide);

            foreach (var row in rows2) {
                obs.Add(row);
            }
            sw.Stop();
            Trace.WriteLine($"Reflection Prop:{sw.ElapsedMilliseconds} - {rows2.Count()}");
            

            obs.Clear();
            sw.Restart();
            var rows3 = models.Where(p => p.Int1 > devide && p.Int2 < devide);
            foreach (var row in rows3) {
                obs.Add(row);
            }
            sw.Stop();
            Trace.WriteLine($"Direct:{sw.ElapsedMilliseconds} -  {rows3.Count()}");

            obs.Clear();
            var methodInfo = typeof(Model).GetMethod(nameof(Model.GetInt));
            var param1 = new object[] { nameof(Model.Int1) };
            var param2 = new object[] { nameof(Model.Int2) };
            sw.Restart();
            var rows4 = list.Where(p => (int)methodInfo.Invoke(p, param1) > devide && (int)methodInfo.Invoke(p, param2) < devide);
            foreach (var row in rows4) {
                obs.Add(row);
            }
            sw.Stop();
            Trace.WriteLine($"Reflection Method:{sw.ElapsedMilliseconds} - {rows4.Count()}");

            
            sw.Restart();
            var rm5list = new List<DataRow>();
            _dt.Rows.Clear();
            foreach (DataRow row in _dt.Rows) {
                if(!((int)row[nameof(IntCol1)] > devide&&
                    (int)row[nameof(IntCol2)] < devide)){
                    rm5list.Add(row);
                }
            }
            rm5list.ForEach(p => _dt.Rows.Remove(p));

            sw.Stop();
            Trace.WriteLine($"DataTable row:{sw.ElapsedMilliseconds} - {_dt.Rows.Count}");
        }

        

    }
   
}
