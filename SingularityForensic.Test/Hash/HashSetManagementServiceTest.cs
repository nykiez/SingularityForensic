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
        public const string TestDescription = "Description";
        public const string TestName = "testSet";

        [TestMethod]
        public void AddHashSetTest() {
            var hashSet = HashSetFactory.CreateNew(
                  DirPath,
                  Guid.NewGuid().ToString("P"),
                  ServiceProvider.GetAllInstances<IHasher>().FirstOrDefault(p => p.GUID == HashGUID)
            );
           
            _hashSetManagementService.ClearHashSets();
            _hashSetManagementService.AddHashSet(hashSet);
            Assert.AreEqual(_hashSetManagementService.HashSets.Count(), 1);
            var hashSet2 = _hashSetManagementService.HashSets.First();
            Assert.AreEqual(hashSet2, hashSet);
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
            var hashSet = _hashSetManagementService.HashSets.First();
            hashSet.Name = TestName;
            hashSet.Description = TestDescription;
            hashSet.IsEnabled = true;

            _hashSetManagementService.Initialize();
            Assert.AreEqual(_hashSetManagementService.HashSets.Count(), 1);

            var hashSet2 = _hashSetManagementService.HashSets.First();

            Assert.AreNotEqual(hashSet, hashSet2);
            
            //通过反射比较属性是否相等;
            var props = hashSet.GetType().GetProperties();
            foreach (var prop in props) {
                var val = prop.GetValue(hashSet);
                var val2 = prop.GetValue(hashSet2);
                Assert.IsTrue(object.Equals(val, val2));
            }
        
        }

    }
}
