using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingularityForensic.Contracts.FileExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.FileExplorer {
    [TestClass]
    public class TestCategoryNameService {

        [TestInitialize]
        public void Initialize() {
            TestCommon.InitializeTest();
            _categoryNameService = CategoryNameService.Current;
            Assert.IsNotNull(_categoryNameService);
            _categoryNameService.Initialize();
        }

        private ICategoryNameService _categoryNameService;
        [TestMethod]
        public void TestLoadKeyPairFrom() {
            CategoryNameService.LoadDescriptorsFromFile("D:\\SingularitySolution\\SingularityForensic\\FileExplorer\\File Type Categories.txt");
        }
        [TestMethod]
        public void TestExpired() {
            var pair = CategoryNameService.GetNameCategory("thumbcache_96.db");
            Assert.IsNotNull(pair);
            Assert.IsFalse(pair.IsExpired);
            CategoryNameService.LoadDescriptorsFromFile("D:\\SingularitySolution\\SingularityForensic\\FileExplorer\\File Type Categories.txt");
            Assert.IsTrue(pair.IsExpired);
        }

        [TestMethod]
        public void TestGetKeyPair() {
            var pair = CategoryNameService.GetNameCategory("thumbcache_96.db");
            Assert.IsNotNull(pair);
            Assert.IsFalse(pair.IsExpired);
            Assert.AreEqual(pair.CategoryName, "Windows 7缩略图");
        }
    }
}
