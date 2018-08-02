using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.NTFS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.NTFS {
    [TestClass()]
    public class UsnRecordV2Tests {
        [TestMethod()]
        public void UsnRecordV2Test() {
            Assert.Fail();
        }
        private const string UsnFileName = "E://anli/UsnJrnl(1)";

        [TestMethod()]
        public void ReadFromStreamTest() {
            var fs = File.OpenRead(UsnFileName);
            UsnRecordV2 record = null;
            while ((record = UsnRecordV2.ReadFromStream(fs)) != null) {
                Trace.WriteLine(record.FileName);
            }
            
            fs.Dispose();
        }

        [TestMethod]
        public void ReadRecordsFromStreamTest() {
            var fs = File.OpenRead(UsnFileName);
            var records = UsnRecordV2.ReadRecordsFromStream(fs);
            foreach (var record in records) {
                //Trace.WriteLine($"{record.FileName}");
            }
            fs.Dispose();
        }
    }
}