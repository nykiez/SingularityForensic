using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Contracts.Common;
using System.Linq;
using System.Collections;

namespace SingularityForensic.Test.Hash {
    /// <summary>
    /// Summary description for HashSetStatusManagementServiceTest
    /// </summary>
    [TestClass]
    public class HashSetStatusManagementServiceTest {

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            CaseService.ConfirmCaseLoaded();
            Assert.IsNotNull(CaseService.Current.CurrentCase);
            _hashSetStatusManagementService = ServiceProvider.GetInstance<IHashSetStatusManagementService>();
            Assert.IsNotNull(_hashSetStatusManagementService);
        }
        IHashSetStatusManagementService _hashSetStatusManagementService;

        private TestContext testContextInstance;
        
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestSetAndGet() {
            var testHashSetGUID = "dadad";
            var testUnitName = "123";
            var testHashSetType = "testType";

            _hashSetStatusManagementService.BeginEdit();
            _hashSetStatusManagementService.DeleteAll();
            _hashSetStatusManagementService.SetUnitHashSetStatus(testUnitName, new string[] { testHashSetGUID }, testHashSetType);
            _hashSetStatusManagementService.EndEdit();
            _hashSetStatusManagementService.BeginOpen();
            var status = _hashSetStatusManagementService.GetAllHashSetStatus();
            
            Assert.IsNotNull(status);
            Assert.AreEqual(status.Count(), 1);

            var sta1 = status.First();

            Assert.AreEqual(sta1.UnitName, testUnitName);
            Assert.AreEqual(sta1.StatusType, testHashSetType);
            Assert.AreEqual(sta1.HashSetGuids.Count(), 1);
            Assert.AreEqual(sta1.HashSetGuids.First(), testHashSetGUID);

            _hashSetStatusManagementService.EndOpen();
        }
    }
}
