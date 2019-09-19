using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.NTFS.LogFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensicTest.NTFS.LogFile {
    [TestClass()]
    public class LogFileRSTRTests {
        [TestMethod()]
        public void LogFileRSTRTest() {
            var fs = File.OpenRead("E://anli/LogFile(1)");
            var buffer = fs.ReadExact(LogFileRSTR.RSTRSize);
            var rstr = new LogFileRSTR(buffer);
            Assert.IsNotNull(rstr.Header);
            Assert.IsNotNull(rstr.RSTRArea);
            Assert.IsNotNull(rstr.RSTRClient);
            fs.Dispose();
        }
    }
}