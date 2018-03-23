using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Test;
using SingularityForensic.Test.Casing;

namespace SingularityForensic.Casing.Commands.Tests {
    [TestClass()]
    public class OpenCasePathCommandItemTests {
        [TestInitialize]
        public void TestInitialize() {
            TestCommon.InitializeTest();
        }

        [TestMethod()]
        public void OpenCasePathCommandItemTest() {
            var cs = new Case(CaseMockers.CaseFolder,CaseMockers.CaseName); 
            var comm = new OpenCasePathCommandItem(cs);
            comm.Command.Execute(null);
        }
    }
}