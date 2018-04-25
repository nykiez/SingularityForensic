using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.ITunes;
using System.Linq;

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
            Assert.IsNotNull(manager);

            Assert.IsNotNull(manager.Directory, null);

            var regFile = manager.Directory.Children.ElementAt(2);
            Assert.IsNotNull(regFile);

            var fs = regFile.GetInputStream();

            
        }
    }
}