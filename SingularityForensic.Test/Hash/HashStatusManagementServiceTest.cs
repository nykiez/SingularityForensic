using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Hash {
    [TestClass]
    public class HashStatusManagementServiceTest {

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            CaseService.ConfirmCaseLoaded();
            Assert.IsNotNull(CaseService.Current.CurrentCase);
            _hashStatusService = ServiceProvider.GetInstance<IHashStatusManagementService>();
            Assert.IsNotNull(_hashStatusService);
            //var cs = new Mock<ICase>();
            //cs.SetupProperty(p => p.Path, "E://anli/Case");
            //var csService = new Mock<ICaseService>();
            //csService.SetupProperty(p => p.CurrentCase, cs.Object);

            //ExportProviderServiceProviderMocker.StaticInstance.SetInstance<ICaseService>(csService.Object);
        }
        IHashStatusManagementService _hashStatusService;
        [TestMethod]
        public void TestSetAndGet() {
            var testName = "testName";
            var testHash = "TestHash";
            var testGUID = "TestGUID";
            var testPairType = "TestType";

            _hashStatusService.BeginEdit();
            _hashStatusService.DeleteAll();
            _hashStatusService.SetFileHashValue(testName, testHash, testGUID, testPairType);
            _hashStatusService.EndEdit();

            _hashStatusService.BeginOpen();
            var pairs = _hashStatusService.GetAllFileHashPairs();
            Assert.AreEqual(pairs.Count(), 1);
            var pair = pairs.First();
            Assert.AreEqual(pair.Name, testName);
            Assert.AreEqual(pair.Value, testHash);
            Assert.AreEqual(pair.HasherGUID, testGUID);
            Assert.AreEqual(pair.PairType, testPairType);
            _hashStatusService.EndOpen();
        }
    }
}
