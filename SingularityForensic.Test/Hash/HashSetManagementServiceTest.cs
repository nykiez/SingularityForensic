using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Hash {
    [TestClass]
    public class HashSetManagementServiceTest {
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _hashSetManagementService = ServiceProvider.GetInstance<IHashSetManagementService>();
            Assert.IsNotNull(_hashSetManagementService);
            _hashSetManagementService.Initialize();
        }

        IHashSetManagementService _hashSetManagementService;

        public const string DirPath = "E://anli//Lucene";
        public const string HashGUID = SingularityForensic.Hash.Constants.HashTypeGUID_MD5;
        [TestMethod]
        public void AddHashSetTest() {
            var hashSet = HashSetFactory.CreateNew(
                  DirPath,
                  Guid.NewGuid().ToString("P"),
                  ServiceProvider.GetAllInstances<IHasher>().FirstOrDefault(p => p.GUID == HashGUID)
            );
            hashSet.Name = "testSet";
            _hashSetManagementService.ClearHashSets();
            _hashSetManagementService.AddHashSet(hashSet);
            Assert.AreEqual(_hashSetManagementService.HashSets.Count(), 1);
        }

        [TestMethod]
        public void ClearHashSetsTest() {
            _hashSetManagementService.ClearHashSets();
            Assert.AreEqual(_hashSetManagementService.HashSets.Count(), 0);
        }

        [TestMethod]
        public void InitializeTest() {
            _hashSetManagementService.ClearHashSets();
            AddHashSetTest();
            _hashSetManagementService.Initialize();
            Assert.AreEqual(_hashSetManagementService.HashSets.Count(), 1);
        }

    }
}
