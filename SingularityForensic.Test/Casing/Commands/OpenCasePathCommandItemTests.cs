using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Casing.Commands;
using SingularityForensic.Test;
using SingularityForensic.Test.Casing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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