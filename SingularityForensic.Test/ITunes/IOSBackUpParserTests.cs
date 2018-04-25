using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.ITunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.ITunes {
    [TestClass()]
    public class IOSBackUpParserTests {

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
        }
        
        [TestMethod()]
        public void DoParseTest() {
            var manager = IOSBackUpParser.DoParse("H://iosb");
        }
    }
}