using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Hash;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.Hash {
    [TestClass()]
    public class HashSetTests {
        public const string DirPath = "E://anli//Lucene";
        public const string HashGUID = SingularityForensic.Hash.Constants.HashTypeGUID_MD5;
        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _hashSet = HashSetFactory.CreateNew(
                DirPath,
                Guid.NewGuid().ToString("P"),
                ServiceProvider.GetAllInstances<IHasher>().FirstOrDefault(p => p.GUID == HashGUID)
            );
            Assert.IsNotNull(_hashSet);
        }
        IHashSet _hashSet;
        //[TestMethod()]
        //public void BeginEditTest() {
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void EndEditTest() {
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void BeginOpenTest() {
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void EndOpenTest() {
        //    Assert.Fail();
        //}
        [TestMethod()]
        public void AddHashPairTest() {
            //查看在BeginEdit前进行调用是否会抛出异常;
            var ex = Assert.ThrowsException<InvalidOperationException>(() => {
                AddHashPairTestCore();
            });
            Assert.IsNotNull(ex);

            _hashSet.BeginEdit();
            _hashSet.Clear();
            AddHashPairTestCore();
            _hashSet.EndEdit();
            _hashSet.BeginOpen();
            var allPairs = _hashSet.GetAllHashPairs();
            Assert.AreEqual(allPairs.Count(), 1);
            Assert.IsNotNull(allPairs.FirstOrDefault(p => p.Name == TestName));
            _hashSet.EndOpen();
        }

        private const string TestName = "atool.org";
        private const string TestMD5 = "54fda78aa8a09b4d77b5aaec57b75028";
        private void AddHashPairTestCore() {
            _hashSet.AddHashPair(TestName, TestMD5);
        }

        [TestMethod()]
        public void GetAllHashPairsTest() {
            AddHashPairTest();
            Assert.ThrowsException<InvalidOperationException>(() => {
                GetAllHashPairsTestCore();
            });
            _hashSet.BeginOpen();
            GetAllHashPairsTestCore();
            var allPairs = _hashSet.GetAllHashPairs();
            Assert.AreEqual(allPairs.Count(), 1);
            _hashSet.EndOpen();
        }

        private void GetAllHashPairsTestCore() {
            var allPairs = _hashSet.GetAllHashPairs();
            foreach (var pair in allPairs) {
                Trace.WriteLine($"{pair.Name} {pair.Value}");
            }
        }

        [TestMethod()]
        public void FindHashPairsTest() {
            AddHashPairTest();
            Assert.ThrowsException<InvalidOperationException>(() => {
                FindHashPairsTestCore();
            });
            _hashSet.BeginOpen();
            FindHashPairsTestCore();
            _hashSet.EndOpen();
            Assert.ThrowsException<InvalidOperationException>(() => {
                FindHashPairsTestCore();
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                 Assert.IsTrue(_hashSet.ContainsValue(TestMD5));
            });
            _hashSet.BeginOpen();
            Assert.IsTrue(_hashSet.ContainsValue(TestMD5));
            _hashSet.EndOpen();
        }

        private void FindHashPairsTestCore() {
            var pairs = _hashSet.FindHashPairs(TestMD5);
            Assert.AreEqual(pairs.Count(), 1);
            Assert.AreEqual(pairs.FirstOrDefault().Value, TestMD5);
        }
        [TestMethod()]
        public void DisposeTest() {
            _hashSet.Dispose();
            Assert.ThrowsException<ObjectDisposedException>(() => {
                AddHashPairTestCore();
            });
            
        }

        /// <summary>
        /// 测试GC与iterator是否正常工作;
        /// </summary>
        [TestMethod]
        public void TestGC() {
            _hashSet.BeginOpen();
            for (int i = 0; i < 100000; i++) {
                var s = _hashSet.FindHashPairs(TestMD5).FirstOrDefault();
            }
            
            for (int i = 0; i < 5; i++) {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            _hashSet.EndOpen();
            System.Threading.Thread.Sleep(10000);
        }
    }
}